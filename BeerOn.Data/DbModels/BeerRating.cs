using System.ComponentModel.DataAnnotations;

namespace BeerOn.Data.DbModels
{
    public class BeerRating : BaseEntity
    {
        [Required]
        public int Flavor { get; set; }

        [Required]
        public int Smell { get; set; }

        [Required]
        public int Appearance { get; set; }

        [Required]
        public int Average { get; set; }

        public int BeerId { get; set; }
        public Beer Beer { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
