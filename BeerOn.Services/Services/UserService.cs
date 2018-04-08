using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto;
using BeerOn.Repo.Interfaces;
using BeerOn.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

//using Microsoft.IdentityModel.Tokens;

namespace BeerOn.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Role> _roleRepository;

        public UserService(IUserRepository userRepository, IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _userRepository.GetUser(username, password);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public User Create(User user, string password)
        {

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            if (!_userRepository.CreateUser(user))
                return null;
            if (!_userRepository.Save())
                return null;

            return user;
        }

        public LoginResponseData GetJwt(User user, string refreshToken, string secret, string accessExpire)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            //====Moj kod do roli ====
            var roles = GetUserRoles(user);
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Id.ToString()) };
            foreach (var v in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, v.Name));
            }
            //========Koniec===========

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(accessExpire)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var response = new LoginResponseData
            {
                AccessToken = tokenString,
                RefreshToken = refreshToken
            };

            return response;
        }

        public bool CheckIfLoginUnique(string login)
        {
            if (_userRepository.CheckLogin(login))
                return false;
            return true;
        }

        public bool CheckIfEmailUnique(string email)
        {
            if (_userRepository.CheckEmail(email))
                return false;
            return true;
        }

        public bool ChangePassword(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return false;

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(changePasswordDto.NewPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return _userRepository.UpdateUser(user);
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);

        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public UserToken GenerateRefreshToken(int userId)
        {
            var token = Guid.NewGuid().ToString().Replace("-", "") + Guid.NewGuid().ToString().Replace("-", "");

            return new UserToken
            {
                UserId = userId,
                Token = token
            };
        }

        public bool AddRefreshToken(UserToken userToken)
        {
            return _userRepository.AddRefreshToken(userToken);

        }

        public UserToken GetRefreshToken(string refreshToken)
        {
            var token = _userRepository.GetRefreshToken(refreshToken);

            if (token == null)
                return null;

            return token;
        }

        public bool DeleteRefreshToken(UserToken userToken)
        {
            return _userRepository.DeleteRefreshToken(userToken); //ToDo Sprawdzic czy dziala
        }

        public bool SendConfirmationEmail(User user)
        {
            try
            {
                var key = GenerateConfirmationKey(user);

                using (SmtpClient client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "mailinteron@gmail.com",
                        Password = "4BsYmh3adsz"
                    };
                    client.Credentials = credential;

                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;

                    var message = new MailMessage();

                    message.To.Add(new MailAddress(user.Email));
                    message.From = new MailAddress("mailinteron@gmail.com");
                    message.Subject = "Link aktywacyjny";
                    message.Body = "http://localhost:58200/users/" + key.UserId + "/" + key.Key + "<br />Link aktywacyjny<br />Klucz : " + key.Key + "<br />UserId : " + key.UserId;
                    message.IsBodyHtml = true;

                    client.Send(message);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool ConfirmEmail(ConfirmationKey key)
        {
            if (key == null)
                return false;

            return _userRepository.ConfirmEmail(key);
        }

        public ConfirmationKey GetConfirmationKey(int userId, string key)
        {
            if (key == null)
            {
                return null;
            }
            return _userRepository.GetConfirmationKey(userId, key);
        }

        public ConfirmationKey GenerateConfirmationKey(User user)
        {
            var key = Guid.NewGuid().ToString().Replace("-", "");

            var confirmationKey = new ConfirmationKey
            {
                UserId = user.Id,
                Key = key,
                Revoked = false
            };

            _userRepository.AddConfirmationKey(confirmationKey);

            return confirmationKey;
        }

        public bool RevokeConfirmationKey(ConfirmationKey key)
        {
            return _userRepository.RevokeConfirmationKey(key);
        }

        public bool DeleteConfirmationKey(ConfirmationKey key)
        {
            return _userRepository.DeleteConfirmationKey(key);
        }

        public bool CheckPassword(string password, int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return false;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return true;

            return false;
        }

        public bool AssignRoleToUser(UserRole userRole)
        {
            var user = _userRepository.GetUserById(userRole.UserId);

            if (user == null)
            {
                return false;
            }

            var role = _roleRepository.Get(userRole.RoleId);

            if (role == null)
            {
                return false;
            }

            if (_userRepository.AssignRoleToUser(userRole) == false)
            {
                return false;
            }

            if (_userRepository.Save() == false)
            {
                return false;
            }

            return true;
        }

        private IEnumerable<Role> GetUserRoles(User user)
        {
            return _userRepository.GetUserRoles(user.Id);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) return false;
            if (string.IsNullOrWhiteSpace(password)) return false;
            if (storedHash.Length != 64) return false;
            if (storedSalt.Length != 128) return false;

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
