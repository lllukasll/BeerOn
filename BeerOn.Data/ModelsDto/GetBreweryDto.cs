using System;
using System.Collections.Generic;
using System.Text;
using BeerOn.Data.DbModels;

namespace BeerOn.Data.ModelsDto
{
    public class GetBreweryDto : BaseEntity
    {
        public string Name { get; set; }
    }
}
