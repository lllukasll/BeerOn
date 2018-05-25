using System.Collections.Generic;
using System.Threading.Tasks;
using BeerOn.Data.ModelsDto.Beer;
using Microsoft.AspNetCore.Http;

namespace BeerOn.Services.Interfaces
{
    public interface IBeerService
    {
        Task<bool> IsBeerExist(int id);
        Task<GetBeerDto> GetBeerAsync(int id);
        Task<IEnumerable<GetBeerDto>> GetAllAsync();
        Task<GetBeerDto> AddBeerAsync(SaveBeerDto beerDto, int userLogged);
        Task RemoveBeer(int id);
        Task<bool> UpdateBeer(int id, SaveBeerDto beerDto);
        Task UploadPhoto(int id, IFormFile file, string uploadsFolderPath);
        Task ConfirmBeerAsync(int id);
    }
}
