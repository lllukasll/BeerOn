using System;
using System.ComponentModel.DataAnnotations;

namespace BeerOn.Data.DbModels
{
    public class Comment : BaseEntity
    {
        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int BeerId { get; set; }
        public Beer Beer { get; set; }
    }
}