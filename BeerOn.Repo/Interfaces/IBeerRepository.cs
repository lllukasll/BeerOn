using System.Collections.Generic;
using System.Threading.Tasks;
using BeerOn.Data.DbModels;

namespace BeerOn.Repo.Interfaces
{
    public interface IBeerRepository : IRepository<Beer>
    {
        Task<Beer> GetBeer(int id);
        Task<IEnumerable<Beer>> GetAllBeers();
        Task<IEnumerable<Beer>> GetHighestRankingBeersAsync();
        Task<IEnumerable<Beer>> GetBeersAddedByUser(int userId);
    }
}
