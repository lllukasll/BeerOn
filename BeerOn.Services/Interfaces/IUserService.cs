using System;
using System.Collections.Generic;
using System.Text;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto;

namespace BeerOn.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        LoginResponseData GetJwt(User user, string refreshToken, string secret, string accessExpire);

        User Authenticate(string username, string password);
        User Create(User user, string password);
        User GetUserById(int userId);

        UserToken GenerateRefreshToken(int userId);
        UserToken GetRefreshToken(string refreshToken);

        ConfirmationKey GetConfirmationKey(int userId, string key);
        ConfirmationKey GenerateConfirmationKey(User user);

        bool CheckIfLoginUnique(string login);
        bool CheckIfEmailUnique(string email);
        bool AddRefreshToken(UserToken userToken);
        bool DeleteRefreshToken(UserToken userToken);
        bool SendConfirmationEmail(User user);
        bool ConfirmEmail(ConfirmationKey key);
        bool RevokeConfirmationKey(ConfirmationKey key);
        bool DeleteConfirmationKey(ConfirmationKey key);
        bool CheckPassword(string password, int userId);
        bool ChangePassword(int userId, ChangePasswordDto changePasswordDto);
        bool AssignRoleToUser(UserRole userRole);

    }
}
