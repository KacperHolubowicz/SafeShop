using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PagingWrapper<IEnumerable<ProductGetListDTO>>>> GetProductsAsync
            ([FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var products = await productService.GetProductsAsync(new ProductPagingFilter(page, size));
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductGetDTO>> GetProductAsync([FromQuery] Guid? cartId, [FromRoute] string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var product = await productService.GetProductAsync(guid, cartId);
                return Ok(product);
            } 
            catch(ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            } 
            catch(FormatException)
            {
                return BadRequest("Invalid format of an id");
            } 
            catch(Exception ex3)
            {
                logger.LogError(ex3.Message);
                logger.LogError(ex3.StackTrace);
                return BadRequest("Invalid request. Seek logs for more information.");
            }
        }

        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> PostProductAsync([FromForm] ProductPostDTO product, [FromForm] IFormFile imageFile)
        {
            if(imageFile.Length == 0)
            {
                return BadRequest("You need to add a valid file with any content");
            }
            try
            {
                product.Image = await FileToByteArray(imageFile);
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProductAsync([FromForm] ProductPutDTO product, [FromRoute] string id, [FromForm] IFormFile imageFile)
        {
            try
            {
                product.Image = await FileToByteArray(imageFile);
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
        
        private async Task<byte[]> FileToByteArray(IFormFile file)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
