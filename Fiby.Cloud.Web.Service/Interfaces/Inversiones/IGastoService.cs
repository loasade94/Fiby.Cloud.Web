using Fiby.Cloud.Web.DTO.Modules.Inversiones.Request;
using Fiby.Cloud.Web.DTO.Modules.Inversiones.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces.Inversiones
{
    public interface IGastoService
    {
        Task<string> RegistrarGasto(GastoDTORequest gastoDTORequest);
        Task<List<GastoDTOResponse>> GetGastoAll(GastoDTORequest gastoDTORequest);
        Task<string> EliminarGasto(GastoDTORequest gastoDTORequest);
        Task<GastoDTOResponse> GetGastoPorCodigo(GastoDTORequest gastoDTORequest);
    }
}
