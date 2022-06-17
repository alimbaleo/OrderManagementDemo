using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Contract.Accounts;
using OrderManagement.Application.Contract.Shared;

namespace OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountsAppService _accountAppService;

        public AccountsController(IAccountsAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Register([FromBody] CreateUserDto createUserDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountAppService.UserRegisteration(createUserDto);
                if (result.Status == false)
                    return StatusCode(result.ResponseCode, result);

                return Ok(result);

            }
            else
            {

                return StatusCode(400, string.Join(" ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Login([FromBody] LoginDto model)
        {
            ResponseDto<LoginResponseDto> result = await _accountAppService.LoginAsync(model);
            if (result.Status == false)
                return StatusCode(result.ResponseCode, result);
            return Ok(result);
        }

    }
}
