using AutoMapper;
using BeerOn.API.Helpers;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto;
using BeerOn.Data.ModelsDto.User;
using BeerOn.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BeerOn.API.Controllers
{
    [Authorize]
    [Route("api")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] LoginUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userDto == null)
                return BadRequest("Niepoprawne dane");


            if (userDto.GrantType == "password")
            {
                if (userDto.Username == null)
                    return BadRequest("Pole nazwa użytkownika jest wymagane.");

                if (userDto.Password == null)
                    return BadRequest("Pole hasło jest wymagane.");

                return DoPassword(userDto);
            }
            else if (userDto.GrantType == "refresh_token")
            {
                if (userDto.UserId == null)
                    return BadRequest("Pole id uzytkownika jest wymagane.");

                if (userDto.RefreshToken == null)
                    return BadRequest("Pole refreshToken jest wymagane.");

                return DoRefreshToken(userDto);
            }
            else
            {
                return BadRequest("Wystąpił problem podczas autentykacji");
            }
        }

        private IActionResult DoPassword(LoginUserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
            {
                return BadRequest("Email i/lub hasło są nie poprawne");
            }

            if (user.EmailConfirmed == false)
            {
                return BadRequest("W celu zalogowania wymagana jest aktywacja konta");
            }

            var refreshToken = _userService.GenerateRefreshToken(user.Id);
            if (refreshToken == null)
            {
                return BadRequest("Wystąpił błąd podczas logowania");
            }

            if (_userService.AddRefreshToken(refreshToken))
            {
                return Ok(_userService.GetJwt(user, refreshToken.Token, _appSettings.Secret, _appSettings.AccessExpireMinutes));
            }

            return BadRequest();
        }

        private IActionResult DoRefreshToken(LoginUserDto userDto)
        {
            var token = _userService.GetRefreshToken(userDto.RefreshToken);
            if (token == null)
            {
                return BadRequest("Nieprawidłowy token");
            }

            var user = _userService.GetUserById(token.UserId);
            if (user == null)
            {
                return BadRequest("Wystąpił problem podczas logowania : User");
            }

            var refreshToken = _userService.GenerateRefreshToken(user.Id);
            if (refreshToken == null)
            {
                return BadRequest("Wystąpił problem podczas logowania : GenerateRefreshToken");
            }

            if (!_userService.DeleteRefreshToken(token))
            {
                return BadRequest("Wystąpił problem podczas logowania : DeleteRefreshToken");
            }

            if (!_userService.AddRefreshToken(refreshToken))
            {
                return BadRequest("Wystąpił problem podczas logowania : AddRefreshToken");
            }

            //var user = _mapper.Map<User>(userDto);

            return Ok(_userService.GetJwt(user, refreshToken.Token, _appSettings.Secret, _appSettings.AccessExpireMinutes));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userService.CheckIfLoginUnique(userDto.Username))
            {
                return BadRequest("Login " + userDto.Username + " jest zajęty");
            }

            if (!_userService.CheckIfEmailUnique(userDto.Email))
            {
                return BadRequest("Email " + userDto.Email + " jest zajęty");
            }

            var user = _mapper.Map<User>(userDto);

            if (_userService.Create(user, userDto.Password) == null)
            {
                return BadRequest("Wystąpił problem podczas rejestracji");
            }

            if (_userService.SendConfirmationEmail(user) == false)
            {
                return BadRequest("Wystąpił problem podczas rejestracji");
            }

            return Ok("Rejestracja przebiegła pomyślnie");
        }

        [AllowAnonymous]
        [HttpGet("{userId}/{key}")]
        public IActionResult ConfirmEmail(int userId, string key)
        {
            var confirmationKey = _userService.GetConfirmationKey(userId, key);

            if (confirmationKey == null)
                return BadRequest();

            var confirmEmailFlag = _userService.ConfirmEmail(confirmationKey);
            var revokeConfirmationKeyFlag = _userService.RevokeConfirmationKey(confirmationKey);

            return Ok();
        }

        [HttpPost("changePassword")]
        public IActionResult ChangePassword([FromBody]ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userLogged = int.Parse(HttpContext.User.Identity.Name);

            if (_userService.GetUserById(userLogged) == null)
            {
                return BadRequest("Brak użytkownika o id : " + userLogged);
            }

            if (changePasswordDto.NewPassword != changePasswordDto.NewPassword2)
            {
                return BadRequest("Hasła nie zgadzają się");
            }

            if (_userService.CheckPassword(changePasswordDto.OldPassword, userLogged))
            {
                return BadRequest("Złe haslo");
            }

            if (_userService.ChangePassword(userLogged, changePasswordDto))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user == null)
                return BadRequest("Brak użytkownika o id : " + userId);

            var mappedUser = _mapper.Map<GetUserDataDto>(user);

            return Ok(mappedUser);
        }

        [HttpGet("getLoggedUser")]
        public IActionResult GetLoggedUser()
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            var user = _userService.GetUserById(userId);

            if (user == null)
                return BadRequest();

            return Ok(user);
        }

        [HttpGet()]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpPost("assignRole")]
        public IActionResult AssignRoleToUser([FromBody]UserRoleDto userRoleDto)
        {
            if (userRoleDto == null)
                return BadRequest("Nieprawidłowe dane");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userRole = _mapper.Map<UserRole>(userRoleDto);

            if (!_userService.AssignRoleToUser(userRole))
            {
                return BadRequest("Wystąpił problem podczas przypisywania roli do użytkownika");
            }

            return Ok(userRole);
        }

    }
}
