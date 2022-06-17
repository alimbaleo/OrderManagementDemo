using AutoMapper;
using OrderManagement.Application.Contract.Orders;
using OrderManagement.Application.Contract.Shared;
using OrderManagement.Application.Helpers;
using OrderManagement.Domain.Contracts;
using OrderManagement.Domain.Entitities;
using OrderManagement.Domain.Exceptions;
using static OrderManagement.Domain.Exceptions.ErrorCodes;
using Microsoft.Extensions.Logging;

namespace OrderManagement.Application
{
    public class OrdersAppService : IOrdersAppService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrdersService _orderService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserInfo _currentUserInfo;
        private readonly ILogger<OrdersAppService> _logger;

        public OrdersAppService(IOrdersRepository playerRepository, IOrdersService ordersService, IMapper mapper, ICurrentUserInfo currentUserInfo, ILogger<OrdersAppService> logger)
        {
            _ordersRepository = playerRepository;
            _orderService = ordersService;
            _mapper = mapper;
            _currentUserInfo = currentUserInfo;
            _logger = logger;
        }

        public async Task<ResponseDto<OrdersDto>> GetOrder(long orderId)
        {
            try
            {
                var currentUser = _currentUserInfo.GetCurrentUserEmail();
                var response = await _ordersRepository.Get(orderId);
                if(response.CreatedBy != currentUser && !_currentUserInfo.IsCurrentUserAdmin())
                {
                    return new ResponseDto<OrdersDto>("This order was not found", RESOURCE_NOT_FOUND);
                }
                return new ResponseDto<OrdersDto>(_mapper.Map<Order, OrdersDto>(response));
            }

            catch (BusinessException ex)
            {
                return new ResponseDto<OrdersDto>(ex.Message, ex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured getting order with order id {orderId}", orderId);
                return new ResponseDto<OrdersDto>("An error occurred. Please refresh and try again", INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<ResponseDto<List<OrdersDto>>> GetOrders(string email = "")
        {          

            try
            {
                if (string.IsNullOrEmpty(email) || !_currentUserInfo.IsCurrentUserAdmin())
                {
                    email = _currentUserInfo.GetCurrentUserEmail();
                }
                var response = await _ordersRepository.GetList(email);
                return new ResponseDto<List<OrdersDto>>(_mapper.Map<List<Order>, List<OrdersDto>>(response));
            }

            catch (BusinessException ex)
            {
                return new ResponseDto<List<OrdersDto>>(ex.Message, ex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured getting order list for user");
                return new ResponseDto<List<OrdersDto>>("An error occurred. Please refresh and try again", INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<ResponseDto<OrdersDto>> RegisterOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                var currentUser = _currentUserInfo.GetCurrentUserEmail();
                var response = await _orderService.RegisterOrder(currentUser, createOrderDto.ProductName, createOrderDto.OrderNo, createOrderDto.Price, createOrderDto.TotalPrice, createOrderDto.Total, createOrderDto.OrderDate );                
                return new ResponseDto<OrdersDto>(_mapper.Map<Order, OrdersDto>(response));
            }

            catch (BusinessException ex)
            {
                return new ResponseDto<OrdersDto>(ex.Message, ex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured creating order with order number {orderNo}", createOrderDto.OrderNo);
                return new ResponseDto<OrdersDto>("An error occurred. Please refresh and try again", INTERNAL_SERVER_ERROR);
            }
        }
    }
}
