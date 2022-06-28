using Microsoft.AspNetCore.Mvc;
using todo_mw.Models;
using todo_mw.Data;

namespace todo_mw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RecipesController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public RecipesController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            return _dbContext.Recipes.OrderByDescending(recipe => recipe.CreateDate);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var recipe = _dbContext.Recipes.Find(id);
            if (recipe != null)
            {
                return Ok(recipe);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
        [HttpGet("byUserId/{id}")]
        public IActionResult GetByUserId(int id)
        {
            int UserId = Convert.ToInt32(id);
            var recipe = _dbContext.Recipes.Where(rcp=>rcp.UserId == UserId);
            if (recipe != null)
            {
                return Ok(recipe);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
        [HttpPost]
        public void Post([FromBody] Recipe recipe)
        {
            recipe.CreateDate = DateTime.UtcNow;
            _dbContext.Recipes.Add(recipe);
            _dbContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Recipe recipeObj)
        {
            var recipe = _dbContext.Recipes.Find(id);
            recipe.Title = recipeObj.Title;
            recipe.Description = recipeObj.Description;
            _dbContext.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var recipe = _dbContext.Recipes.Find(id);
            _dbContext.Recipes.Remove(recipe);
            _dbContext.SaveChanges();
        }
    }
}