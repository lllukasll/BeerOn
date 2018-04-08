using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeerOn.Data.DbModels;
using BeerOn.Repo.Interfaces;
using BeerOn.Services.Interfaces;

namespace BeerOn.Services.Services
{
    public class BeerTypeService : IBeerTypeService
    {
        private readonly IRepository<BeerType> _beerTypeRepository;

        public BeerTypeService(IRepository<BeerType> beerTypeRepository)
        {
            _beerTypeRepository = beerTypeRepository;
        }

        public async Task<bool> AddBeerType(BeerType beerType)
        {
            if (beerType == null)
            {
                return false;
            }

            if (await _beerTypeRepository.AddAsyn(beerType) == null)
            {
                return false;
            }

            return true;
        }

        public async Task<BeerType> GetBeerType(int id)
        {
            return await _beerTypeRepository.GetAsync(id);
        }

        public async Task<IEnumerable<BeerType>> GetAll()
        {
            return await _beerTypeRepository.GetAllAsyn();
        }

        public async Task<bool> RemoveBeerType(BeerType beerType)
        {
            if (await _beerTypeRepository.DeleteAsyn(beerType) == 0)
            {
                return false;
            }
            return true;
        }
    }
}
