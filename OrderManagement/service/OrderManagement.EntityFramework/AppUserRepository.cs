using Microsoft.EntityFrameworkCore;
using OrderManagement.EntityFramework.Identity;

namespace OrderManagement.EntityFramework
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly OrderManagementDBContext _OrderManagementDBContext;
        public AppUserRepository(OrderManagementDBContext OrderManagementDBContext)
        {
            _OrderManagementDBContext = OrderManagementDBContext;
        }

        public async Task<List<AppUser>> GetList()
        {
        return await _OrderManagementDBContext.AppUsers.ToListAsync();
        }

        public async Task<bool> UserExists(string email)
        {
            return await _OrderManagementDBContext.AppUsers.AnyAsync(x => x.Id == email.ToLower());
        }
    }
}
