using Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Request;
using Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces.Mantenimiento
{
    public interface IPuestoService
    {
        Task<List<PuestoDTOResponse>> GetPuestoAll(PuestoDTORequest puestoDTORequest);
        //Task<string> GuardarPuesto(PuestoDTORequest puestoDTORequest);
        //Task<string> EditarPuesto(PuestoDTORequest puestoDTORequest);
        //Task<PuestoDTOResponse> GetPuestoPorId(PuestoDTORequest puestoDTORequest);
        //Task<string> EliminarPuesto(PuestoDTORequest puestoDTORequest);
    }
}
