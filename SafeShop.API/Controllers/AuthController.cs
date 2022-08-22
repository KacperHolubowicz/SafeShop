using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafeShop.Service.DTO.User;
using SafeShop.Service.Infrastructure;

namespace SafeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //TODO add JWT service
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<AuthController> logger;

        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] UserPostDTO credentials)
        {
            try
            {
                await userService.PostUserAsync(credentials);
                return NoContent();
            } catch(DuplicateWaitObjectException)
            {
                return BadRequest("There is already a user with given login or email");
            } catch(Exception ex)
            {
                logger.LogError(ex.StackTrace);
                logger.LogError(ex.Message);
                return BadRequest("An error has occured while registering your account");
            }
        }
    }
}
