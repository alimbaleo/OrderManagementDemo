using AutoMapper;
using OrderManagement.Application.Contract.AppUsers;
using OrderManagement.Application.Contract.Orders;
using OrderManagement.Domain.Entitities;
using OrderManagement.EntityFramework.Identity;

namespace OrderManagement.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<Order, OrdersDto>();
        }
    }
}
