using System.Threading.Tasks;
using BeerOn.Data.DbModels;

namespace BeerOn.Repo.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<bool> IfExistBeer(int beerId);
    }
}