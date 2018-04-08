using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOn.Data.DbModels
{
    public class BeerRating : BaseEntity
    {
        public int Flavor { get; set; }
        public int Smell { get; set; }
        public int Appearance { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
