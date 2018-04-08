using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeerOn.Data.DbModels;

namespace BeerOn.Repo.Interfaces
{
    public interface IBeerRepository : IRepository<Beer>
    {
        Task<Beer> GetBeer(int id);
        Task<List<Beer>> GetAllBeers();
    }
}
