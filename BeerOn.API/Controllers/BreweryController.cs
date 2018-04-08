using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BeerOn.Data.DbModels;
using BeerOn.Data.ModelsDto;
using BeerOn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeerOn.API.Controllers
{
    [Route("api/brewery")]
    public class BreweryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBreweryService _breweryService;

        public BreweryController(IMapper mapper, IBreweryService breweryService)
        {
            _mapper = mapper;
            _breweryService = breweryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrewery(int id)
        {
            var brewery = await _breweryService.GetBrewery(id);
            if (brewery == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetBreweryDto>(brewery));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var breweries = await _breweryService.GetAll();

            return Ok(_mapper.Map<IEnumerable<GetBreweryDto>>(breweries));
        }

        [HttpPost]
        public async Task<IActionResult> AddBrewery([FromBody] SaveBreweryDto breweryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brewery = _mapper.Map<SaveBreweryDto, Brewery>(breweryDto);
            if (!await _breweryService.AddBrewery(brewery))
            {
                return BadRequest("Wystąpił problem podczas dodawania browaru");
            }

            return Ok(breweryDto);
        }

        //[HttpPut("{breweryId}")]
        //public async Task<IActionResult> UpdateBrewery(int breweryId,[FromBody] SaveBreweryDto breweryDto)
        //{
            
        //}
    }
}
