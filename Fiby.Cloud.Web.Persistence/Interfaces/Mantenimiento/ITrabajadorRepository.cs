using Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Request;
using Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces.Mantenimiento
{
    public interface ITrabajadorRepository
    {
        Task<List<TrabajadorDTOResponse>> GetTrabajadorAll(TrabajadorDTORequest trabajadorDTORequest);
        Task<string> GuardarTrabajador(TrabajadorDTORequest trabajadorDTORequest);
        Task<string> EditarTrabajador(TrabajadorDTORequest trabajadorDTORequest);
        Task<TrabajadorDTOResponse> GetTrabajadorPorId(TrabajadorDTORequest trabajadorDTORequest);
        Task<string> EliminarTrabajador(TrabajadorDTORequest trabajadorDTORequest);
    }
}
