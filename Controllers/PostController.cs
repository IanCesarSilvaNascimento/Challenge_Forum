using ForumApi.Data;
using ForumApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Controller
{

    [ApiController]
    [Route("")]
    public class PostController : ControllerBase
    {

        [HttpGet("v1/posts")]
        public async Task<IActionResult> GetAsync([FromServices] ForumApiDataContext context)
            => Ok(await context.Posts.ToListAsync());

        [HttpGet("v1/posts/{id:int}")]
        public async Task<IActionResult> GetIdAsync([FromRoute] int id, [FromServices] ForumApiDataContext context)
        {

            var post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        [HttpPost("v1/posts")]
        public async Task<IActionResult> PostAsync([FromBody] Post model, [FromServices] ForumApiDataContext context)
        {
            await context.Posts.AddAsync(model);
            await context.SaveChangesAsync();

            return Created($"v1/posts/{model.Id}", model);
        }


        [HttpPut("v1/posts/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Post model, [FromServices] ForumApiDataContext context)
        {

            var post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (post == null)
                return NotFound();
            post.Title = model.Title;
            post.Summary = model.Summary;
            post.Slug = model.Slug;
            post.CreateDate = model.CreateDate;
            post.LastUpdateDate = model.LastUpdateDate;
            post.Category = model.Category;
            context.Posts.Update(post);
            await context.SaveChangesAsync();

            return Ok(post);
        }


        [HttpDelete("v1/post/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] ForumApiDataContext context)
        {

            var post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (post == null)
                return NotFound();

            context.Posts.Remove(post);
            await context.SaveChangesAsync();

            return Ok(post);
        }





    }
}