namespace OrderManagement.EntityFramework.Identity
{
    public interface IAppUserRepository
    {
        Task<bool> UserExists(string email);
        Task<List<AppUser>> GetList();
    }
}
