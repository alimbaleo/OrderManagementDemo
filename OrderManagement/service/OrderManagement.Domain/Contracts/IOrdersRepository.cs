using OrderManagement.Domain.Entitities;

namespace OrderManagement.Domain.Contracts
{
    public interface IOrdersRepository
    {
        Task<Order> Create(Order order);
        Task<Order> Get(long orderId);
        Task<Order> GetByOrderNo(string orderNo);
        Task<List<Order>> GetList(string createdBy = "");

    }
}
