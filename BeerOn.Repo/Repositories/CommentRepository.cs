using System.Threading.Tasks;
using BeerOn.Data.DbModels;
using BeerOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeerOn.Repo.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> IfExistBeer(int beerId)
        {
            return await _context.Beers.AnyAsync(a => a.Id == beerId);
        }
    }
}