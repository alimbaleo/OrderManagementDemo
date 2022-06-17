using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Contracts;
using OrderManagement.Domain.Entitities;
using static OrderManagement.Domain.Exceptions.ErrorCodes;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.EntityFramework
{
    public class OrderRepository : IOrdersRepository
    {
     private readonly OrderManagementDBContext _orderManagementDBContext;
        public OrderRepository(OrderManagementDBContext orderManagementDBContext)
        {
            _orderManagementDBContext = orderManagementDBContext;
        }

        public async Task<Order> Create(Order order)
        {
            await _orderManagementDBContext.Orders.AddAsync(order);
            await _orderManagementDBContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Get(long orderId)
        {
            return await _orderManagementDBContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId) ?? throw new BusinessException(RESOURCE_NOT_FOUND, "This order does not exist");
        }

        public async Task<Order> GetByOrderNo(string orderNo) => await _orderManagementDBContext.Orders.FirstOrDefaultAsync(x => x.OrderNo == orderNo);

        public async Task<List<Order>> GetList(string createdBy = "")
        {
            if(!string.IsNullOrEmpty(createdBy))
                return await _orderManagementDBContext.Orders.Where(x => x.CreatedBy == createdBy).ToListAsync();

            return await _orderManagementDBContext.Orders.ToListAsync();
        }
    }
}
