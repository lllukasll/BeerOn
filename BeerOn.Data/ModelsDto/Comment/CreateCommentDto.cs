using System;
using System.ComponentModel.DataAnnotations;

namespace BeerOn.Data.ModelsDto.Comment
{
    public class SaveCommentDto
    {
        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public DateTime UpdateDateTime { get; set; }
    }
}