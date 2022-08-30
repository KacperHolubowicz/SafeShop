using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafeShop.Repository.Filters;
using SafeShop.Service.DTO.Product;
using SafeShop.Service.Exceptions;
using SafeShop.Service.Infrastructure;

namespace SafeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            this.productService = productService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PagingWrapper<IEnumerable<ProductGetListDTO>>>> GetProductsAsync
            ([FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var products = await productService.GetProductsAsync(new ProductPagingFilter(page, size));
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductGetDTO>> GetProductAsync([FromQuery] Guid? cartId, [FromRoute] string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var product = await productService.GetProductAsync(guid, cartId);
                return Ok(product);
            } catch(ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch(FormatException)
            {
                return BadRequest("Invalid format of an id");
            } catch(Exception ex3)
            {
                logger.LogError(ex3.Message);
                logger.LogError(ex3.StackTrace);
                return BadRequest("Invalid request. Seek logs for more information.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostProductAsync([FromBody] ProductPostDTO product)
        {
            try
            {
                await productService.PostProductAsync(product);
                return NoContent();
            }
            catch (AddingResourceException ex)
            {
                logger.LogError(ex.Message);
                logger.LogError(ex.StackTrace);
                return BadRequest("Error with adding a product. Seek logs for more information.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProductAsync([FromBody] ProductPutDTO product, [FromRoute] string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                await productService.PutProductAsync(product, guid);
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
        public async Task<ActionResult> DeleteProductAsync([FromRoute] string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                await productService.DeleteProductAsync(guid);
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
