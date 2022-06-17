using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Contract.Shared;
using OrderManagement.Application.Contract.Orders;

namespace OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrdersAppService _orderAppService;

        public OrdersController(IOrdersAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ResponseDto<OrdersDto>>> Get(long id)
        {
            ResponseDto<OrdersDto> result = await _orderAppService.GetOrder(id);
            return StatusCode(result.ResponseCode, result);
        }
        [HttpGet]
        [Route("GetList")]
        public async Task<ActionResult<ResponseDto<List<OrdersDto>>>> GetList(string email)
        {
            ResponseDto<List<OrdersDto>> result = await _orderAppService.GetOrders(email);
            return StatusCode(result.ResponseCode, result);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<ResponseDto<OrdersDto>>> UpdatePlayerInfo([FromBody]CreateOrderDto input)
        {
            ResponseDto<OrdersDto> result = await _orderAppService.RegisterOrder(input);
            return StatusCode(result.ResponseCode, result);
        }
    }
}

