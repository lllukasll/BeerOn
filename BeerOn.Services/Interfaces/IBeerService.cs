using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeerOn.Data.DbModels;

namespace BeerOn.Services.Interfaces
{
    public interface IBeerService
    {
        Task<Beer> GetBeer(int id);
        Task<IEnumerable<Beer>> GetAll();
        Task<bool> AddBeer(Beer beer);
        Task<bool> RemoveBeer(Beer beer);
        Task<bool> UpdateBeer(int id, Beer beer);
    }
}
