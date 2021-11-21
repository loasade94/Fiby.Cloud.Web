using Fiby.Cloud.Web.DTO.Modules.Ple.Request;
using Fiby.Cloud.Web.DTO.Modules.Ple.Response;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Modules.Data.Interfaces
{
    public interface IPleService
    {
        Task<string> GetPleAll(PleDTORequest pleDTORequest);
        Task<List<PleDTOResponse>> GetPleAllNew(PLE14100DTORequest pLE14100DTORequest);
        Task<string> RegistrarPle14100DPorMes(PLE14100DTORequest pLE14100DTORequest);
        Task<List<PLE14100DTOResponse>> GetPlePLE14100All(PLE14100DTORequest pLE14100DTORequest);
    }
}
