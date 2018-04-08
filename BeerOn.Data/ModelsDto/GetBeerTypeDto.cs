using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BeerOn.Data.DbModels;

namespace BeerOn.Data.ModelsDto
{
    public class GetBeerTypeDto : BaseEntity
    {
        public string Name { get; set; }
    }
}
