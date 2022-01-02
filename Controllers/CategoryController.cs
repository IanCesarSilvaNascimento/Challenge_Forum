using ForumApi.Data;
using ForumApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Controller
{

    [ApiController]
    [Route("")]
    public class CategoryController : ControllerBase
    {

        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices] ForumApiDataContext context)
            => Ok(await context.Categories.ToListAsync());

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetIdAsync([FromRoute] int id, [FromServices] ForumApiDataContext context)
        {

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] Category model, [FromServices] ForumApiDataContext context)
        {
            await context.Categories.AddAsync(model);
            await context.SaveChangesAsync();

            return Created($"v1/categories/{model.Id}", model);
        }


        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Category model, [FromServices] ForumApiDataContext context)
        {

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            category.Name = model.Name;
            category.Slug = model.Slug;
            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return Ok(category);
        }


        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] ForumApiDataContext context)
        {

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok(category);
        }





    }
}