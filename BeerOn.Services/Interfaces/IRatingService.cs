using System.Collections.Generic;
using System.Threading.Tasks;
using BeerOn.Data.ModelsDto.Beer;

namespace BeerOn.Services.Interfaces
{
    public interface IRatingService
    {
        Task<bool> IfBeerExistAsync(int beerId);
        Task<GetBeerRatingDto> GetBeerRatingAsync(int beerId);
        Task<GetBeerRatingDto> AddBeerRatingAsync(int userLogged, int beerId, AddBeerRatingDto beerRatingDto);
        Task<GetBeerRatingDto> GetBeerRaitingForUserAsync(int userLogged, int beerId);
        Task<bool> IfExistRating(int ratingId);
        Task UpdateRatingAsync(int ratingId, AddBeerRatingDto ratingDto);
    }
}