using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafeShop.Repository.Filters;
using SafeShop.Service.DTO.Product;
using SafeShop.Service.Infrastructure;

namespace SafeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductGetListDTO>>> GetProductsAsync([FromQuery] int page, [FromQuery] int size)
        {
            var products = await productService.GetProductsAsync(new ProductPagingFilter(page, size));
            return Ok(products);
        }
    }
}
