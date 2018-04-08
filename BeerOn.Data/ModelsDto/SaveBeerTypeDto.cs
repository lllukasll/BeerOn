using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOn.Data.ModelsDto
{
    public class SaveBeerTypeDto
    {
        [Required]
        public string Name { get; set; }
    }
}
