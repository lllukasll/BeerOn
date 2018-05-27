using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeerOn.Data.DbModels;
using BeerOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeerOn.Repo.Repositories
{
    public class RatingRepository : Repository<BeerRating>, IRatingRepository
    {
        public RatingRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> IfExistBeer(int beerId)
        {
            return await _context.Beers.AnyAsync(a => a.Id == beerId);
        }
    }
}
