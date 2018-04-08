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
    [Route("api/beerTypes")]
    public class BeerTypeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBeerTypeService _beerTypeService;

        public BeerTypeController(IMapper mapper, IBeerTypeService beerTypeService)
        {
            _mapper = mapper;
            _beerTypeService = beerTypeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeerType(int id)
        {
            var beerType = await _beerTypeService.GetBeerType(id);
            if (beerType == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetBeerTypeDto>(beerType));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var beerTypes = await _beerTypeService.GetAll();

            return Ok(_mapper.Map<IEnumerable<GetBeerTypeDto>>(beerTypes));
        }

        [HttpPost]
        public async Task<IActionResult> AddBeerType([FromBody] SaveBeerTypeDto beerTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var beerType = _mapper.Map<SaveBeerTypeDto, BeerType>(beerTypeDto);
            if (!await _beerTypeService.AddBeerType(beerType))
            {
                return BadRequest("Wystąpił problem podczas dodawania typu piwa");
            }

            return Ok(beerTypeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeerType(int id)
        {
            var beerType = await _beerTypeService.GetBeerType(id);
            if (beerType == null)
            {
                return NotFound();
            }

            if (!await _beerTypeService.RemoveBeerType(beerType))
            {
                return BadRequest("Wystąpił problem podczas usuwania typu piwa");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeerType(int id, [FromBody] SaveBeerTypeDto beerTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var beerType = await _beerTypeService.GetBeerType(id);
            if (beerType == null)
            {
                return NotFound();
            }

            var mappedBeerType = _mapper.Map(beerTypeDto, beerType);

            if (!await _beerTypeService.UpdateBeerType(id, mappedBeerType))
            {
                return BadRequest("Wystąpił problem podczas modyfikacji typu piwa");
            }

            return Ok();

        }


    }
}
