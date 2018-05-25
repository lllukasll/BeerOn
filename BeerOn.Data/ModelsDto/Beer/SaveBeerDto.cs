using System.ComponentModel.DataAnnotations;

namespace BeerOn.Data.ModelsDto.Beer
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
