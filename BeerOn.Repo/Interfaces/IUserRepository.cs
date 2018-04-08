using System.Collections.Generic;
using BeerOn.Data.DbModels;

namespace BeerOn.Repo.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<Role> GetUserRoles(int userId);

        User GetUser(string username, string password);
        User GetUserById(int id);

        UserToken GetRefreshToken(string refreshToken);

        ConfirmationKey GetConfirmationKey(int userId, string key);

        bool CreateUser(User user);
        bool Save();
        bool CheckLogin(string username);
        bool CheckEmail(string email);
        bool UpdateUser(User user);
        bool AddRefreshToken(UserToken userToken);
        bool DeleteRefreshToken(UserToken userToken);
        bool ConfirmEmail(ConfirmationKey key);
        bool AddConfirmationKey(ConfirmationKey key);
        bool RevokeConfirmationKey(ConfirmationKey key);
        bool DeleteConfirmationKey(ConfirmationKey key);
        bool AssignRoleToUser(UserRole userRole);
    }
}