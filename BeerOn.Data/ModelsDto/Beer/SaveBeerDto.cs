using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BeerOn.Data.DbModels;

namespace BeerOn.Data.ModelsDto
{
    public class SaveBeerDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Percentage { get; set; }

        public string AvatarUrl { get; set; }

        [Required]
        public int BreweryId { get; set; }

        [Required]
        public int BeerTypeId { get; set; }
    }
}
