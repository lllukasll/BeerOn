using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto;
using BeerOn.Repo.Interfaces;
using BeerOn.Services.Interfaces;

namespace BeerOn.Services.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IRepository<Brewery> _breweryRepository;

        public BreweryService(IRepository<Brewery> breweryRepository)
        {
            _breweryRepository = breweryRepository;
        }

        public async Task<bool> AddBrewery(Brewery brewery)
        {
            if (brewery == null)
            {
                return false;
            }

            if (await _breweryRepository.AddAsyn(brewery) == null)
            {
                return false;
            }

            return true;
        }

        public async Task<Brewery> GetBrewery(int id)
        {
            return await _breweryRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Brewery>> GetAll()
        {
            return await _breweryRepository.GetAllAsyn();
        }

        public async Task<bool> RemoveBrewery(Brewery brewery)
        {
            if (await _breweryRepository.DeleteAsyn(brewery) == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateBrewery(int id, Brewery brewery)
        {
            if (await _breweryRepository.UpdateAsyn(brewery, id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
