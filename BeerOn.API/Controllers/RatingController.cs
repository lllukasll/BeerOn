using System.Threading.Tasks;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto.Beer;
using BeerOn.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeerOn.API.Controllers
{
    
    public class RatingController : Controller
    {
        private readonly IRatingService _service;
        private readonly IBeerService _beerService;

        public RatingController(IRatingService service, IBeerService beerService)
        {
            _service = service;
            _beerService = beerService;
        }

        [HttpGet]
        [Route("api/beer/{beerId}/rating")]
        public async Task<IActionResult> GetBeerRating(int beerId)
        {
            if (!await _service.IfBeerExistAsync(beerId))
            {
                return NotFound();
            }

            var result = await _service.GetBeerRatingAsync(beerId);

            return Ok(result);
        }

        [HttpGet]
        [Route("api/beer/{beerId}/rating/user")]
        public async Task<IActionResult> GetUserRating(int beerId)
        {
            var userLogged = int.Parse(HttpContext.User.Identity.Name);

            var userRating = await _service.GetBeerRaitingForUserAsync(userLogged, beerId);

            return Ok(userRating);
        }

        [Authorize]
        [HttpPost]
        [Route("api/beer/{beerId}/rating")]
        public async Task<IActionResult> AddRating(int beerId, [FromBody] AddBeerRatingDto ratingDto)
        {
            if (!await _service.IfBeerExistAsync(beerId))
            {
                return NotFound();
            }

            var userLogged = int.Parse(HttpContext.User.Identity.Name);

            var userRating = await _service.GetBeerRaitingForUserAsync(userLogged, beerId);

            if (userRating == null)
            {
                var result = await _service.AddBeerRatingAsync(userLogged, beerId, ratingDto);

                var beer = await _beerService.GetBeerAsync(beerId);

                var updatedBeer = new SaveBeerDto();

                updatedBeer.Name = beer.Name;
                updatedBeer.AvatarUrl = beer.AvatarUrl;
                updatedBeer.BeerTypeId = beer.BeerType.Id;
                updatedBeer.BreweryId = beer.Brewery.Id;
                updatedBeer.Description = beer.Description;
                updatedBeer.Percentage = beer.Percentage;
                if (beer.AverageRating == 0)
                {
                    updatedBeer.AverageRating = (beer.AverageRating + ratingDto.Average);
                }
                else
                {
                    updatedBeer.AverageRating = (beer.AverageRating + ratingDto.Average ) / 2;
                }
                

                var updateResult = await _beerService.UpdateBeer(beerId, updatedBeer);

                if (!updateResult)
                    return BadRequest("Niepowodzenie :(");

                return Ok(result);
            }

            return BadRequest("Juz dodałeś ocenę");
        }

        [Authorize]
        [Route("api/beerRating/{ratingId}")]
        public async Task<IActionResult> UpdateRating(int ratingId, [FromBody] AddBeerRatingDto ratingDto)
        {
            if (!await _service.IfExistRating(ratingId))
            {
                return NotFound();
            }
            var beerId = await _service.UpdateRatingAsync(ratingId, ratingDto);

            GetBeerRatingDto beerRating = await _service.GetBeerRatingAsync(beerId);

            var beer = await _beerService.GetBeerAsync(beerId);

            var updatedBeer = new SaveBeerDto();

            updatedBeer.Name = beer.Name;
            updatedBeer.AvatarUrl = beer.AvatarUrl;
            updatedBeer.BeerTypeId = beer.BeerType.Id;
            updatedBeer.BreweryId = beer.Brewery.Id;
            updatedBeer.Description = beer.Description;
            updatedBeer.Percentage = beer.Percentage;
            if (beer.AverageRating == 0)
            {
                updatedBeer.AverageRating = beerRating.Average;
            }
            else
            {
                updatedBeer.AverageRating = beerRating.Average;
            }


            var updateResult = await _beerService.UpdateBeer(beerId, updatedBeer);

            if (!updateResult)
                return BadRequest("Niepowodzenie :(");

            return Ok();
        }
    }
}