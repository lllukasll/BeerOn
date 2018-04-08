using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeerOn.Data.DbModels;
using BeerOn.Repo.Interfaces;
using BeerOn.Services.Interfaces;

namespace BeerOn.Services.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository;

        public BeerService(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public async Task<bool> AddBeer(Beer beer)
        {
            if (beer == null)
            {
                return false;
            }

            if (await _beerRepository.AddAsyn(beer) == null)
            {
                return false;
            }

            return true;
        }

        public async Task<Beer> GetBeer(int id)
        {
            //return await _beerRepository.GetAsync(id);
            return await _beerRepository.GetBeer(id);
        }

        public async Task<IEnumerable<Beer>> GetAll()
        {
            //return await _beerRepository.GetAllAsyn();
            return await _beerRepository.GetAllBeers();
        }

        public async Task<bool> RemoveBeer(Beer beer)
        {
            if (await _beerRepository.DeleteAsyn(beer) == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateBeer(int id, Beer beer)
        {
            if (await _beerRepository.UpdateAsyn(beer, id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
