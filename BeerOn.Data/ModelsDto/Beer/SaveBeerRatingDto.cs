using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOn.Data.ModelsDto.Beer
{
    public class SaveBeerRatingDto
    {
        [Required]
        public int Flavor { get; set; }

        [Required]
        public int Smell { get; set; }

        [Required]
        public int Appearance { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
