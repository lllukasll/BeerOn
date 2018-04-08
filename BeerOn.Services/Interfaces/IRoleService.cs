using System.Collections.Generic;
using BeerOn.Data.DbModels;

namespace BeerOn.Services.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetRole(long id);
        void InsertRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(long id);
    }
}