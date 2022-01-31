using Fiby.Cloud.Web.DTO.Modules.Clinica.Request;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces.Clinica
{
    public interface ICitaService
    {
        Task<List<CitaDTOResponse>> GetCitaAll(CitaDTORequest citaDTORequest);
        Task<string> GuardarCita(CitaDTORequest citaDTORequest);
        Task<string> EditarCita(CitaDTORequest citaDTORequest);
        Task<CitaDTOResponse> GetCitaPorId(CitaDTORequest citaDTORequest);
        Task<string> EliminarCita(CitaDTORequest citaDTORequest);
    }
}
