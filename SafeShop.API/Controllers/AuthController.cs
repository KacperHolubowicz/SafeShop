using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeShop.Service.DTO.Auth;
using SafeShop.Service.DTO.User;
using SafeShop.Service.Infrastructure;

namespace SafeShop.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<AuthController> logger;
        private readonly ILoginService loginService;

        public AuthController(IUserService userService, ILogger<AuthController> logger, ILoginService loginService)
        {
            this.userService = userService;
            this.logger = logger;
            this.loginService = loginService;
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

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync([FromBody] LoginCredentials credentials)
        {
            try
            {
                var user = await loginService.LoginAsync(credentials);
                string token = loginService.GenerateJWT(user);
                return Ok(token);
            } catch(InvalidCredentialsException ex)
            {
                return NotFound(ex.Message);
            } catch (Exception ex2)
            {
                logger.LogError(ex2.StackTrace);
                logger.LogError(ex2.Message);
                return BadRequest("An error has occured while signing in");
            }
        }
    }
}
