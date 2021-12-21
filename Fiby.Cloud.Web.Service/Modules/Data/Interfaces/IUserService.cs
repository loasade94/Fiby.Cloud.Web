using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Modules.Data.Interfaces
{
    public interface IUserService
    {
        Task<string> LoginUser(UserDTORequest userDTORequest);
        Task<UserDTOResponse> GetUserLogin(UserDTORequest userDTORequest);
        Task<string> LoginUserNew(UserDTORequest userDTORequest);
        Task<UserDTOResponse> GetUserLoginNew(UserDTORequest userDTORequest);
        Task<string> RegistarLogIngreso(UserDTORequest userDTORequest);
    }
}
