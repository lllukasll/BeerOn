using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto;
using BeerOn.Data.ModelsDto.Beer;
using BeerOn.Repo.Interfaces;
using BeerOn.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BeerOn.Services.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IMapper _mapper;

        public BeerService(IBeerRepository beerRepository,IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<GetBeerDto> AddBeerAsync(SaveBeerDto beerDto)
        {
            var beer = _mapper.Map<SaveBeerDto, Beer>(beerDto);
            await _beerRepository.AddAsyn(beer);
            await _beerRepository.SaveAsync();

            return await GetBeerAsync(beer.Id);

        }

        public async Task<GetBeerDto> GetBeerAsync(int id)
        {
           
           var beer = await _beerRepository.GetBeer(id);
           var result = _mapper.Map<Beer, GetBeerDto>(beer);
            return result;
        }

        public async Task<IEnumerable<GetBeerDto>> GetAllAsync()
        {
           var beers = await _beerRepository.GetAllBeers();
            var result = _mapper.Map<IEnumerable<GetBeerDto>>(beers);
           return result;

        }

        public async Task RemoveBeer(int id)
        {
            var beer = await _beerRepository.GetAsync(id);
            await _beerRepository.DeleteAsyn(beer);
        }
        public async Task<bool> IsBeerExist(int id)
        {
            var beer = await _beerRepository.GetAsync(id);
            if (beer==null)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> UpdateBeer(int id, SaveBeerDto beerDto)
        {
            var beer = await _beerRepository.GetBeer(id);
            var mappedBeer = _mapper.Map(beerDto, beer);
            if (await _beerRepository.UpdateAsyn(mappedBeer, id) == null)
            {
                return false;
            }
            return true;
        }

        public async Task UploadPhoto(int id, IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var beer = await _beerRepository.GetAsync(id);
            var avatar = new UploadAvatarDto
            {
                AvatarUrl = $"{fileName}"
            };
            _mapper.Map(avatar, beer);
            await _beerRepository.SaveAsync();
        }
    }
}
