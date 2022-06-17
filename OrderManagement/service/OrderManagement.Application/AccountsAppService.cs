using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Application.Contract.Accounts;
using OrderManagement.Application.Contract.Shared;
using OrderManagement.EntityFramework.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static OrderManagement.Domain.Exceptions.ErrorCodes;
using static OrderManagement.Domain.Constants;

namespace OrderManagement.Application
{
    public class AccountsAppService : IAccountsAppService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAppUserRepository _userRepository;
        private readonly ILogger<AccountsAppService> _logger;

        public AccountsAppService(UserManager<AppUser> userManager, IConfiguration configuration, IAppUserRepository userRepository, ILogger<AccountsAppService> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto loginDto)
        {
            try
            {

                var user = await _userManager.FindByNameAsync(loginDto.Email);
                if (user == null)
                {
                    return new ResponseDto<LoginResponseDto>("A user with this email was not found", RESOURCE_NOT_FOUND);
                }
                else if (await _userManager.CheckPasswordAsync(user, loginDto.Password))
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    return new ResponseDto<LoginResponseDto>(GenerateToken(user.Email, user.FirstName, user.Surname, roles.FirstOrDefault()));
                }

                return new ResponseDto<LoginResponseDto>("Incorrent email or password", UNAUTHORIZED_OPERATION);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured logging in user with id {userId}", loginDto.Email);
                return new ResponseDto<LoginResponseDto>("An error occurred. Please refresh and try again", INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<ResponseDto<LoginResponseDto>> UserRegisteration(CreateUserDto createUserDto)
        {
            try
            {


                var userExist = await _userRepository.UserExists(createUserDto.Email);
                if (userExist)
                    return new ResponseDto<LoginResponseDto>("A user with the email specified already exists", CONFLICT);

                AppUser user = new()
                {
                    Email = createUserDto.Email.ToLower(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = createUserDto.Email,
                    FirstName = createUserDto.FirstName,
                    Surname = createUserDto.Surname,
                    Id = createUserDto.Email.ToLower()
                };

                var result = await _userManager.CreateAsync(user, createUserDto.Password);
                if (!result.Succeeded)
                {
                    var errorAndCode = GetError(result.Errors);
                    return new ResponseDto<LoginResponseDto>(errorAndCode.errors, errorAndCode.code);
                }
                await _userManager.AddToRoleAsync(user, USER_ROLE);

                return new ResponseDto<LoginResponseDto>(GenerateToken(user.Email, user.FirstName, user.Surname, USER_ROLE));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured registering user with id {userId}", createUserDto.Email);
                return new ResponseDto<LoginResponseDto>("An error occurred. Please refresh and try again", INTERNAL_SERVER_ERROR);
            }
        }



        private (string errors, int code) GetError(IEnumerable<IdentityError> identityErrors)
        {
            var codeString = identityErrors.FirstOrDefault()?.Code ?? BAD_REQUEST.ToString();
            if(!int.TryParse(codeString, out int errorCode))
            {
                errorCode = BAD_REQUEST;
            }
            var errorMessages = identityErrors.Select(x => x.Description);
            string error = $"The following error(s) occured: {string.Join(" ", errorMessages)}";
            return (error, errorCode);
        }

        private LoginResponseDto GenerateToken(string email, string firstName, string surname, string role)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.GivenName, firstName),
                    new Claim(ClaimTypes.Surname, surname),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(double.Parse(_configuration["JWT:ExpiryInHours"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new LoginResponseDto(new JwtSecurityTokenHandler().WriteToken(token), role, firstName, surname, email, token.ValidTo);
        }

        public Task<ResponseDto<string>> AddAdminUser(CreateUserDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
