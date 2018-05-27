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

        public RatingController(IRatingService service)
        {
            _service = service;
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
            await _service.UpdateRatingAsync(ratingId, ratingDto);

            return Ok();
        }
    }
}