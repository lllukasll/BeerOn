using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto;

namespace BeerOn.Services.Interfaces
{
    public interface IBreweryService
    {
        Task<Brewery> GetBrewery(int id);
        Task<IEnumerable<Brewery>> GetAll();
        Task<bool> AddBrewery(Brewery brewery);
        Task<bool> RemoveBrewery(Brewery brewery);
        Task<bool> UpdateBrewery(int id, Brewery brewery);
    }
}
