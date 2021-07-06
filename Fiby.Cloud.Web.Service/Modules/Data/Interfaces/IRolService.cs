using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Modules.Data.Interfaces
{
    public interface IRolService
    {
        Task<List<RolDTOResponse>> GetRolAll(RolDTORequest rolDTORequest);
        Task<string> RegisterRol(RolDTORequest rolDTORequest);
        Task<string> DeleteRol(RolDTORequest rolDTORequest);
        Task<RolDTOResponse> GetRolById(RolDTORequest rolDTORequest);
        Task<string> UpdateRol(RolDTORequest rolDTORequest);
    }
}
