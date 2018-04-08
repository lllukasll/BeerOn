using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BeerOn.Data.DbModels;

namespace BeerOn.Data.ModelsDto
{
    public class AddBeerDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        [MaxLength(10)]
        public string Percentage { get; set; }

        [Required]
        public int BreweryId { get; set; }

        [Required]
        public int BeerTypeId { get; set; }
    }
}
