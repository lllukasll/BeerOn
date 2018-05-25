using System.IO;
using System.Threading.Tasks;
using BeerOn.API.Helpers;
using BeerOn.Data.ModelsDto.Beer;
using BeerOn.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BeerOn.API.Controllers
{
    [Route("api/beer")]
    public class BeerController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IBeerService _service;
        private readonly PhotoSettings _photoSettings;

        public BeerController(IOptions<PhotoSettings> options, IHostingEnvironment host, IBeerService beerService)
        {
            _host = host;
            _service = beerService;
            _photoSettings = options.Value;
        }
        [HttpPost]
        [Route("{id}/photo")]
        public async Task<IActionResult> Upload(int id, IFormFile file)
        {
            if (await _service.IsBeerExist(id) == false) return NotFound();
          
            if (file == null) return BadRequest("Brak Pliku");
            if (file.Length == 0) return BadRequest("Pusty plik");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Za duży plik");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Nieprawidłowy typ");

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            await _service.UploadPhoto(id, file, uploadsFolderPath);
            return Ok();
        }
        [HttpGet("{id}",Name = "GetBeer")]
        public async Task<IActionResult> GetBeer(int id)
        {
            var beer = await _service.GetBeerAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            return Ok(beer);
        }
        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> ConfirmBeer(int id)
        {
            if (await _service.IsBeerExist(id) == false) return NotFound();
            await _service.ConfirmBeerAsync(id);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var beers = await _service.GetAllAsync();
            if (beers == null)
            {
                return NotFound();
            }
            return Ok(beers);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddBeer([FromBody] SaveBeerDto beerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userLogged = int.Parse(HttpContext.User.Identity.Name);

            var result = await _service.AddBeerAsync(beerDto,userLogged);

            return CreatedAtRoute("GetBeer", new {id = result.Id}, result);
        }
           
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            if (!await _service.IsBeerExist(id))
            {
                return NotFound();
            }

            await _service.RemoveBeer(id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeer(int id, [FromBody] SaveBeerDto beerDto)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            if (!await _service.IsBeerExist(id))
            {
                return NotFound();
            }

           

            if (!await _service.UpdateBeer(id, beerDto))
            {
                return BadRequest("Wystąpił problem podczas modyfikacji typu piwa");
            }

            return Ok();

        }

        //Beer Rating
        //[HttpPost("{beerId}/rate")]
        //public async Task<IActionResult> AddBeerRating(int beerId, [FromBody] SaveBeerRatingDto beerRatingDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (_beerService.GetBeer(beerId) == null)
        //    {
        //        return NotFound();
        //    }

        //    var mappedBeerRating = _mapper.Map<SaveBeerRatingDto, BeerRating>(beerRatingDto);
        //    if (!await _beerService.AddBeerRating(mappedBeerRating))
        //    {
        //        return BadRequest("Wystąpił problem podczas dodawania oceny");
        //    }

        //    return Ok();
        //}
    }
}
