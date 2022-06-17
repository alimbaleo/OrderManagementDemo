using Microsoft.AspNetCore.Identity;

namespace OrderManagement.EntityFramework.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}
