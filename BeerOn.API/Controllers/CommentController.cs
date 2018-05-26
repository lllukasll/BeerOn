using System.Threading.Tasks;
using BeerOn.Data.ModelsDto.Comment;
using BeerOn.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeerOn.API.Controllers
{
    [Route("api/beer/{beerId}/comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }

        [HttpGet("{commentId}",Name = "GetComment")]
        public async Task<IActionResult> GetComment(int beerId,int commentId)
        {
            if (!await _service.IfBeerExistAsync(beerId))
            {
                return NotFound();
            }
            if (!await _service.IfExistComment(commentId))
            {
                return NotFound();
            }

            var result = await _service.GetCommentAsync(commentId);

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetComments(int beerId)
        {
            if (!await _service.IfBeerExistAsync(beerId))
            {
                return NotFound();
            }

            var result = await _service.GetCommentsAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(int beerId,[FromBody] SaveCommentDto commentDto)
        {
            if (!await _service.IfBeerExistAsync(beerId))
            {
                return NotFound();
            }

            var userLogged = int.Parse(HttpContext.User.Identity.Name);

            var result = await _service.CreateCommentAsync(userLogged, beerId, commentDto);

            return CreatedAtRoute("GetComment", new {beerId, commentId = result.Id}, result); 
        }
        [Authorize]
        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateComment(int beerId,int commentId, [FromBody] SaveCommentDto commentDto)
        {
            if (!await _service.IfBeerExistAsync(beerId))
            {
                return NotFound();
            }
            if (!await _service.IfExistComment(commentId))
            {
                return NotFound();
            }
            await _service.UpdateCommentAsync(commentId, commentDto);

            return Ok();
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId, int beerId)
        {
            if (!await _service.IfExistComment(commentId))
            {
                return NotFound();
            }
            if (!await _service.IfBeerExistAsync(beerId))
            {
                return NotFound();
            }

            await _service.RemoveComment(commentId);
            return Ok();
        }
    }
}