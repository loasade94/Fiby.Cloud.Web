using Fiby.Cloud.Web.DTO.Modules.Ple.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces
{
    public interface IPleRepository
    {
        Task<List<PleDTOResponse>> GetPleAll(PleDTORequest pleDTORequest);
        Task<string> RegistrarPle14100DPorMes(PLE14100DTORequest pLE14100DTORequest);
    }
}
