using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeShop.Service.DTO.User;
using SafeShop.Service.Exceptions;
using SafeShop.Service.Infrastructure;

namespace SafeShop.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ResourceClaim")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UsersController> logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDTO>> GetUserAsync([FromRoute] string id)
        {
            try
            {
                Guid userId = Guid.Parse(id);
                var user = await userService.GetUserAsync(userId);
                return Ok(user);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FormatException)
            {
                return BadRequest("Invalid format of an id");
            }
            catch (Exception ex3)
            {
                logger.LogError(ex3.Message);
                logger.LogError(ex3.StackTrace);
                return BadRequest("Invalid request. Seek logs for more information.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUserAsync([FromBody] UserPutDTO user, [FromRoute] string id)
        {
            try
            {
                var userId = Guid.Parse(id);
                await userService.PutUserAsync(user, userId);
                return NoContent();
            }
            catch (FormatException)
            {
                return BadRequest("Invalid format of an id");
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UpdatingResourceException ex3)
            {
                logger.LogError(ex3.Message);
                logger.LogError(ex3.StackTrace);
                return BadRequest("Invalid request. Seek logs for more information.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] string id)
        {
            try
            {
                var userId = Guid.Parse(id);
                await userService.DeleteUserAsync(userId);
                return NoContent();
            }
            catch (FormatException)
            {
                return BadRequest("Invalid format of an id");
            }
            catch (ResourceNotFoundException ex)
            {
                logger.LogError(ex.Message);
                logger.LogError(ex.StackTrace);
                return BadRequest("Invalid request. Seek logs for more information.");
            }
        }

    }
}
