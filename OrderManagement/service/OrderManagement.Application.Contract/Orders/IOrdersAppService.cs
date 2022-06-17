using OrderManagement.Application.Contract.Shared;

namespace OrderManagement.Application.Contract.Orders
{
    public interface IOrdersAppService
    {
        Task<ResponseDto<OrdersDto>> RegisterOrder(CreateOrderDto createOrderDto);
        Task<ResponseDto<List<OrdersDto>>> GetOrders(string email = "");
        Task<ResponseDto<OrdersDto>> GetOrder(long orderId);
    }
}
