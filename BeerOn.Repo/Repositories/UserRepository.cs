using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeerOn.Data.DbModels;
using BeerOn.Repo.Interfaces;

namespace BeerOn.Repo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User GetUser(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            if (user == null)
            {
                user = _context.Users.SingleOrDefault(x => x.Email == username);
            }

            return user;
        }

        public User GetUserById(int id)
        {
            return _context.Users.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.AsEnumerable();
        }

        public bool CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool CheckLogin(string username)
        {
            return _context.Users.Any(x => x.Username == username);
        }

        public bool CheckEmail(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }

        public bool AddRefreshToken(UserToken userToken)
        {
            try
            {
                _context.UserTokens.Add(userToken);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public UserToken GetRefreshToken(string refreshToken)
        {
            return _context.UserTokens.SingleOrDefault(x => x.Token == refreshToken);
        }

        public bool DeleteRefreshToken(UserToken userToken)
        {
            try
            {
                _context.UserTokens.Remove(userToken);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool ConfirmEmail(ConfirmationKey key)
        {
            var user = GetUserById(key.UserId);
            if (user == null)
                return false;

            var tmpUser = user;
            tmpUser.EmailConfirmed = true;

            if (!UpdateUser(tmpUser))
                return false;

            key.Revoked = true;

            if (!RevokeConfirmationKey(key))
                return false;

            return true;
        }

        public ConfirmationKey GetConfirmationKey(int userId, string key)
        {
            return _context.ConfirmationKeys.SingleOrDefault(x => x.Key == key && x.UserId == userId);
        }

        public bool AddConfirmationKey(ConfirmationKey key)
        {
            try
            {
                _context.ConfirmationKeys.Add(key);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool RevokeConfirmationKey(ConfirmationKey key)
        {
            try
            {
                _context.ConfirmationKeys.Update(key);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteConfirmationKey(ConfirmationKey key)
        {
            try
            {
                _context.ConfirmationKeys.Remove(key);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool AssignRoleToUser(UserRole userRole)
        {
            try
            {
                _context.UserRoles.Add(userRole);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Role> GetUserRoles(int userId)
        {
            var roles = _context.UserRoles.Where(c => c.Roles.Id == userId).AsEnumerable();

            List<Role> tmp = new List<Role>();
            foreach (var v in roles)
            {
                var role = _context.Roles.SingleOrDefault(c => c.Id == v.RoleId);
                tmp.Add(role);
            }

            return tmp;
        }
    }
}
