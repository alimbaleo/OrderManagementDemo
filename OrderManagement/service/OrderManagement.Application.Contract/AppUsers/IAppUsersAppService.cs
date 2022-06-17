using OrderManagement.Application.Contract.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Contract.AppUsers
{
    public interface IAppUsersAppService
    {
        Task<ResponseDto<List<AppUserDto>>> GetAppUsers();
    }
}
