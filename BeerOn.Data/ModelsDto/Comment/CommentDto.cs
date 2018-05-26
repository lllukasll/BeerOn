using System;
using BeerOn.Data.ModelsDto.User;

namespace BeerOn.Data.ModelsDto.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDateTime { get; set; }

        public DateTime UpdateDateTime { get; set; }
        public GetUserDto User { get; set; }
    }
}