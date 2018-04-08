using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOn.Data.DbModels
{
    public class Beer : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Percentage { get; set; }
        public string AvatarUrl { get; set; }


        public int BreweryId { get; set; }
        public Brewery Brewery { get; set; }

        public int BeerTypeId { get; set; }
        public BeerType BeerType { get; set; }
    }
}
