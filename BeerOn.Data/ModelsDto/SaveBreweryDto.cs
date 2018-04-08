using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOn.Data.ModelsDto
{
    public class SaveBreweryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
