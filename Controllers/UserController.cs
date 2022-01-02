using ForumApi.Data;
using ForumApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Controller
{

    [ApiController]
    [Route("")]
    public class UserController : ControllerBase
    {

        [HttpGet("v1/users")]
        public async Task<IActionResult> GetAsync([FromServices] ForumApiDataContext context)
            => Ok(await context.Users.ToListAsync());

        [HttpGet("v1/users/{id:int}")]
        public async Task<IActionResult> GetIdAsync([FromRoute] int id, [FromServices] ForumApiDataContext context)
        {

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("v1/users")]
        public async Task<IActionResult> PostAsync([FromBody] User model, [FromServices] ForumApiDataContext context)
        {
            await context.Users.AddAsync(model);
            await context.SaveChangesAsync();

            return Created($"v1/users/{model.Id}", model);
        }


        [HttpPut("v1/users/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] User model, [FromServices] ForumApiDataContext context)
        {

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();
            user.Name = model.Name;
            user.Email = model.Email;
            user.Slug = model.Slug;
            user.PasswordHash = model.PasswordHash;
            user.Image = model.Image;
            user.Slug = model.Slug;
            user.Posts = model.Posts;
            user.Bio = model.Bio;
            context.Users.Update(user);
            await context.SaveChangesAsync();

            return Ok(user);
        }


        [HttpDelete("v1/users/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] ForumApiDataContext context)
        {

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return Ok(user);
        }





    }
}