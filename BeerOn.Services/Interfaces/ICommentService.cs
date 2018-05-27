using System.Collections.Generic;
using System.Threading.Tasks;
using BeerOn.Data.ModelsDto.Comment;

namespace BeerOn.Services.Interfaces
{
    public interface ICommentService
    {
        Task<bool> IfExistComment(int commentId);
        Task RemoveComment(int commentId);
        Task<IEnumerable<CommentDto>> GetCommentsAsync(int beerId);
        Task<bool> IfBeerExistAsync(int beerId);
        Task<CommentDto> GetCommentAsync(int commentId);
        Task<CommentDto> CreateCommentAsync(int userLogged, int beerId, SaveCommentDto commentDto);
        Task UpdateCommentAsync(int beerId, SaveCommentDto commentDto);
    }
}