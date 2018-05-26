using System;
using System.Collections.Generic;
using System.Text;
using BeerOn.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BeerOn.Repo
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<ConfirmationKey> ConfirmationKeys { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<BeerType> BeerTypes { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
