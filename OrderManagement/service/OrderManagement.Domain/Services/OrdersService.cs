using OrderManagement.Domain.Contracts;
using OrderManagement.Domain.Entitities;
using static OrderManagement.Domain.Exceptions.ErrorCodes;
using OrderManagement.Domain.Exceptions;
namespace OrderManagement.Domain.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        public OrdersService(IOrdersRepository playerRepository)
        {
            _ordersRepository = playerRepository;
        }

        public async Task<Order> RegisterOrder(string createdBy, string product, string orderNo, decimal price, decimal totalPrice, decimal total, DateTime orderDate)
        {
            ValidatePrice(price, total, totalPrice);
            ValidateOrderDate(orderDate);

            if (await _ordersRepository.GetByOrderNo(orderNo) != null)
            {
                throw new BusinessException(CONFLICT, "An Order with this number already exists");
            }

            var order = new Order(createdBy, product, orderNo, price, totalPrice, total, orderDate);

            await _ordersRepository.Create(order);
            return order;
        }

        private void ValidatePrice(decimal price, decimal total, decimal totalPrice)
        {
            if(price <= 0)
            {
                throw new BusinessException(INVALID_OPERATION, "Price cannot be zero or less");
            }
            if(total <= 0)
            {
                throw new BusinessException(INVALID_OPERATION, "Total cannot be zero or less");
            }
            if(totalPrice <= 0)
            {
                throw new BusinessException(INVALID_OPERATION, "Total price cannot be zero or less");
            }
            if (price > total || price > totalPrice)
            {
                throw new BusinessException(INVALID_OPERATION, "Price cannot be more than the total price");
            }
        }
        private void ValidateOrderDate(DateTime orderDate)
        {
            if (orderDate > DateTime.Now)
            {
                throw new BusinessException(INVALID_OPERATION, "Order date cannot be in the future");
            }
        }
    }
}
