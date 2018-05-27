using System;
using System.Collections.Generic;
using System.Text;
using BeerOn.Data.ModelsDto.User;

namespace BeerOn.Data.ModelsDto.Beer
{
    public class GetBeerRatingDto
    {
        public int Id { get; set; }

        public int Flavor { get; set; }
        public int Smell { get; set; }
        public int Appearance { get; set; }
        public int Average { get; set; }

        public int NumberOfRaitings { get; set; }
    }
}
