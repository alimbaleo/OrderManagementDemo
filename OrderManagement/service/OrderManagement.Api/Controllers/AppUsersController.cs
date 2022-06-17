using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Contract.Shared;
using OrderManagement.Application.Contract.Orders;
using OrderManagement.Application.Contract.AppUsers;

namespace OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {

        private readonly IAppUsersAppService _appUsersAppService;

        public AppUsersController(IAppUsersAppService appUsersAppService)
        {
            _appUsersAppService = appUsersAppService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<ActionResult<ResponseDto<OrdersDto>>> GetPlayer()
        {
            var result = await _appUsersAppService.GetAppUsers();
            return StatusCode(result.ResponseCode, result);
        }
    }
}

