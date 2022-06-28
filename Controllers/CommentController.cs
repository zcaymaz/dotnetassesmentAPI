using Microsoft.AspNetCore.Mvc;
using todo_mw.Models;
using todo_mw.Data;

namespace todo_mw.Controllers
{
    [ApiController]

    public class CommentController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public CommentController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("api/[controller]/byUserAndRecipeId")]
        public IActionResult AddComment([FromBody]PostComment commentObj)
        {
            Comment comment = new Comment();

            comment.UserId = commentObj.UserId;
            comment.RecipeId = commentObj.RecipeId;
            comment.ZComment = commentObj.ZComment;
            comment.UserName = commentObj.UserName;

            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("api/[controller]/byRecipeId/{id}")]
        public IActionResult GetComments(int id)
        {
            var comments = _dbContext.Comments.Where(cmm => cmm.RecipeId == id);
            
            if(comments != null) {
                return Ok(comments);
            } else {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}