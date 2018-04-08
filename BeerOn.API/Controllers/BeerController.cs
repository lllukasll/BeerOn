using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto;
using BeerOn.Data.ModelsDto.Beer;
using BeerOn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeerOn.API.Controllers
{
    [Route("api/beer")]
    public class BeerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBeerService _beerService;

        public BeerController(IMapper mapper, IBeerService beerService)
        {
            _mapper = mapper;
            _beerService = beerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeer(int id)
        {
            var beer = await _beerService.GetBeer(id);
            if (beer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetBeerDto>(beer));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var beers = await _beerService.GetAll();

            return Ok(_mapper.Map<IEnumerable<GetBeerDto>>(beers));
        }

        [HttpPost]
        public async Task<IActionResult> AddBeer([FromBody] SaveBeerDto beerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var beer = _mapper.Map<SaveBeerDto, Beer>(beerDto);
            if (!await _beerService.AddBeer(beer))
            {
                return BadRequest("Wystąpił problem podczas dodawania piwa");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            var beer = await _beerService.GetBeer(id);
            if (beer == null)
            {
                return NotFound();
            }

            if (!await _beerService.RemoveBeer(beer))
            {
                return BadRequest("Wystąpił problem podczas usuwania piwa");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeer(int id, [FromBody] SaveBeerDto beerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var beer = await _beerService.GetBeer(id);
            if (beer == null)
            {
                return NotFound();
            }

            var mappedBeer = _mapper.Map(beerDto, beer);

            if (!await _beerService.UpdateBeer(id, mappedBeer))
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
