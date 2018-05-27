using System.Threading.Tasks;
using BeerOn.Data.DbModels;

namespace BeerOn.Repo.Interfaces
{
    public interface IRatingRepository : IRepository<BeerRating>
    {
        Task<bool> IfExistBeer(int beerId);
    }
}