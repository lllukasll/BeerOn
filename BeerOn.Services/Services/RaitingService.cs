using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto.Beer;
using BeerOn.Data.ModelsDto.Comment;
using BeerOn.Repo.Interfaces;
using BeerOn.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeerOn.Services.Services
{
    public class RaitingService : IRatingService
    {
        private readonly IRatingRepository _repository;
        private readonly IMapper _mapper;

        public RaitingService(IRatingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetBeerRatingDto> GetBeerRaitingForUserAsync(int userLogged, int beerId)
        {
            var rating = await _repository
                .GetAllIncluding(a => a.User)
                .Where(c => c.BeerId == beerId)
                .Where(d => d.UserId == userLogged)
                .SingleOrDefaultAsync();

            if (rating == null)
                return null;

            GetBeerRatingDto beerRatingDto = new GetBeerRatingDto();
            beerRatingDto.Appearance = rating.Appearance;
            beerRatingDto.Average = rating.Average;
            beerRatingDto.Flavor = rating.Flavor;
            beerRatingDto.Smell = rating.Smell;
            beerRatingDto.NumberOfRaitings = 1;
            beerRatingDto.Id = rating.Id;

            return beerRatingDto;
        }

        public async Task<GetBeerRatingDto> AddBeerRatingAsync(int userLogged, int beerId, AddBeerRatingDto beerRatingDto)
        {
            var rating = _mapper.Map<AddBeerRatingDto, BeerRating>(beerRatingDto);
            rating.BeerId = beerId;
            rating.UserId = userLogged;
            await _repository.AddAsyn(rating);
            await _repository.SaveAsync();

            var result = await GetBeerRaitingForUserAsync(userLogged, beerId);

            return result;
        }

        public async Task<GetBeerRatingDto> GetBeerRatingAsync(int beerId)
        {
            var rating = await _repository
                .GetAllIncluding(a => a.User)
                .Where(c => c.BeerId == beerId)
                .ToListAsync();

            GetBeerRatingDto beerRating = new GetBeerRatingDto();
            beerRating.Appearance = 0;
            beerRating.Average = 0;
            beerRating.Flavor = 0;
            beerRating.Smell = 0;
            beerRating.NumberOfRaitings = rating.Count;
            beerRating.Id = 0;

            if (rating.Count == 0)
                return beerRating;

            foreach (var r in rating)
            {
                beerRating.Appearance += r.Appearance;
                beerRating.Average += r.Average;
                beerRating.Flavor += r.Flavor;
                beerRating.Smell += r.Smell;
            }

            beerRating.Appearance = beerRating.Appearance / rating.Count;
            beerRating.Average = beerRating.Average / rating.Count;
            beerRating.Flavor = beerRating.Flavor / rating.Count;
            beerRating.Smell = beerRating.Smell / rating.Count;

            return beerRating;
        }

        public async Task<bool> IfBeerExistAsync(int beerId)
        {
            return await _repository.IfExistBeer(beerId);
        }

        public async Task<bool> IfExistRating(int ratingId)
        {
            var rating = await _repository.GetAsync(ratingId);
            return rating != null;
        }

        public async Task UpdateRatingAsync(int ratingId, AddBeerRatingDto ratingDto)
        {
            var rating = await _repository.GetAsync(ratingId);
            _mapper.Map(ratingDto, rating);
            await _repository.SaveAsync();
        }
    }
}
