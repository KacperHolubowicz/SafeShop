using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafeShop.Service.DTO.Order;
using SafeShop.Service.Exceptions;
using SafeShop.Service.Infrastructure;

namespace SafeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly ILogger<OrderController> logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            this.orderService = orderService;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ResourceClaim")]
        public async Task<ActionResult<IEnumerable<OrderListDTO>>> GetOrdersAsync([FromRoute] string id) 
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var orders = await orderService.GetOrdersAsync(guid);
                return Ok(orders);
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

        [HttpGet("{id}/{orderId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ResourceClaim")]
        public async Task<ActionResult<OrderGetDTO>> GetOrderAsync([FromRoute] string id, [FromRoute] string orderId)
        {
            try
            {
                Guid userGuid = Guid.Parse(id);
                Guid orderGuid = Guid.Parse(orderId);
                var order = await orderService.GetOrderAsync(orderGuid, userGuid);
                return Ok(order);
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

        [HttpPost]
        public async Task<ActionResult> PostOrderAsync([FromBody] OrderPostDTO order)
        {
            try
            {
                await orderService.PostOrderAsync(order);
                return NoContent();
            }
            catch (AddingResourceException ex)
            {
                logger.LogError(ex.Message);
                logger.LogError(ex.StackTrace);
                return BadRequest("Error with adding a product. Seek logs for more information.");
            }
        }
    }
}
