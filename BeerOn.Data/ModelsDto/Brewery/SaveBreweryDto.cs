using System.ComponentModel.DataAnnotations;

namespace BeerOn.Data.ModelsDto.Brewery
{
    public class SaveBreweryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
