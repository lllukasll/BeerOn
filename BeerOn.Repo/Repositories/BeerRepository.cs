using System.Collections.Generic;
using System.Threading.Tasks;
using BeerOn.Data.DbModels;
using BeerOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeerOn.Repo.Repositories
{
    public class BeerRepository : Repository<Beer>, IBeerRepository
    {
        public BeerRepository(DataContext context) : base(context)
        {

        }

        public async Task<Beer> GetBeer(int id)
        {
            return await _context.Beers.Include(g => g.Brewery).Include(gc => gc.BeerType)
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Beer>> GetAllBeers()
        {
            return await _context.Beers.Include(g => g.Brewery).Include(gc => gc.BeerType).ToListAsync();
        }
        
    }
}
