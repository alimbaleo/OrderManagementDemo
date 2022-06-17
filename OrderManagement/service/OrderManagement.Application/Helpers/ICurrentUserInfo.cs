namespace OrderManagement.Application.Helpers
{
    public interface ICurrentUserInfo
    {
        string GetCurrentUserEmail();
        bool IsCurrentUserAdmin();
    }
}
