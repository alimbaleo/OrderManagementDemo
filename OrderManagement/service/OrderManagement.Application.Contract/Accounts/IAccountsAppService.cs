using OrderManagement.Application.Contract.Shared;

namespace OrderManagement.Application.Contract.Accounts
{
    public interface IAccountsAppService
    {
        Task<ResponseDto<LoginResponseDto>> UserRegisteration(CreateUserDto registerDto);
        Task<ResponseDto<string>> AddAdminUser(CreateUserDto registerDto);
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto loginDto);
    }
}
