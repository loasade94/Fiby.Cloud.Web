using Fiby.Cloud.Web.DTO.Modules.Clinica.Request;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces.Clinica
{
    public interface IPacienteRepository
    {
        Task<PacienteDTOResponse> GetPacientePorDocumento(PacienteDTORequest PacienteDTORequest);
        Task<List<PacienteDTOResponse>> GetPacienteAll(PacienteDTORequest pacienteDTORequest);
        Task<string> GuardarPaciente(PacienteDTORequest pacienteDTORequest);
        Task<string> EditarPaciente(PacienteDTORequest pacienteDTORequest);
        Task<PacienteDTOResponse> GetPacientePorId(PacienteDTORequest pacienteDTORequest);
        Task<string> EliminarPaciente(PacienteDTORequest pacienteDTORequest);
    }
}
