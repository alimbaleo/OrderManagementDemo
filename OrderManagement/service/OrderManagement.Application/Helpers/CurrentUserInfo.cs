using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using static OrderManagement.Domain.Constants;

namespace OrderManagement.Application.Helpers
{
    public class CurrentUserInfo : ICurrentUserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserEmail()
        {
            var userClaims = _httpContextAccessor.HttpContext.User.Claims;
            var emailClaim = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email);

            if ( string.IsNullOrEmpty(emailClaim?.Value))
            {
                throw new Exception("Invalid login data, please login and try again");
            }

            return emailClaim.Value;
        }



        public bool IsCurrentUserAdmin()
        {
            var userClaims = _httpContextAccessor.HttpContext.User.Claims;
            var emailClaim = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            if (string.IsNullOrEmpty(emailClaim?.Value))
            {
                throw new Exception("Invalid login data, please login and try again");
            }

            return emailClaim.Value == ADMIN_ROLE;
        }
    }
}
