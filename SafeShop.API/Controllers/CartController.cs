using Microsoft.AspNetCore.Mvc;
using SafeShop.Service.DTO.Cart;
using SafeShop.Service.Exceptions;
using SafeShop.Service.Infrastructure;

namespace SafeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;
        private readonly ILogger<CartController> logger;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            this.cartService = cartService;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartGetDTO>> GetCartAsync([FromRoute] string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var product = await cartService.GetCartAsync(guid);
                return Ok(product);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCartAsync([FromRoute] string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                await cartService.DeleteCartAsync(guid);
                return NoContent();
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
    }
}
