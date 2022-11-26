using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafeShop.API.Responses;
using SafeShop.Service.DTO.CartProduct;
using SafeShop.Service.Exceptions;
using SafeShop.Service.Infrastructure;

namespace SafeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartProductsController : ControllerBase
    {
        private readonly ICartProductService cartProductService;
        private readonly ILogger<CartProductsController> logger;

        public CartProductsController(ICartProductService cartProductService, ILogger<CartProductsController> logger)
        {
            this.cartProductService = cartProductService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<AddProductToCartResponse>> PostCartProductAsync([FromBody] CartProductPostDTO product)
        {
            try
            {
                Guid cartId = await cartProductService.PostCartProductAsync(product, product.UserID);
                var response = new AddProductToCartResponse()
                {
                    CartID = cartId
                };
                return Ok(response);
            }
            catch (AddingResourceException ex)
            {
                logger.LogError(ex.Message);
                logger.LogError(ex.StackTrace);
                return BadRequest("Error with adding a product. Seek logs for more information.");
            }
            catch (FormatException)
            {
                return BadRequest("Invalid format of an id");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCartProductAsync([FromRoute] string id, [FromBody] CartProductPutDTO cartProduct)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                await cartProductService.PutCartProductAsync(cartProduct, guid);
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
        public async Task<ActionResult> DeleteCartProductAsync([FromRoute] string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                await cartProductService.DeleteCartProductAsync(guid);
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
