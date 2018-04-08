using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeerOn.Data.DbModels;

namespace BeerOn.Services.Interfaces
{
    public interface IBeerTypeService
    {
        Task<BeerType> GetBeerType(int id);
        Task<IEnumerable<BeerType>> GetAll();
        Task<bool> AddBeerType(BeerType brewery);
        Task<bool> RemoveBeerType(BeerType beerType);
    }
}
