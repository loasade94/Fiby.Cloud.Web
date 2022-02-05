using Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Request;
using Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Mantenimiento;
using Fiby.Cloud.Web.Service.Interfaces.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Mantenimiento
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }
        public async Task<PacienteDTOResponse> GetPacientePorDocumento(PacienteDTORequest pacienteDTORequest)
        {
            var response = await _pacienteRepository.GetPacientePorDocumento(pacienteDTORequest);
            return response;
        }

        public async Task<List<PacienteDTOResponse>> GetPacienteAll(PacienteDTORequest pacienteDTORequest)
        {
            var response = await _pacienteRepository.GetPacienteAll(pacienteDTORequest);
            return response;
        }

        public async Task<string> GuardarPaciente(PacienteDTORequest pacienteDTORequest)
        {
            var response = await _pacienteRepository.GuardarPaciente(pacienteDTORequest);
            return response;
        }

        public async Task<string> EditarPaciente(PacienteDTORequest pacienteDTORequest)
        {
            var response = await _pacienteRepository.EditarPaciente(pacienteDTORequest);
            return response;
        }

        public async Task<PacienteDTOResponse> GetPacientePorId(PacienteDTORequest pacienteDTORequest)
        {
            var response = await _pacienteRepository.GetPacientePorId(pacienteDTORequest);
            return response;
        }
        public async Task<string> EliminarPaciente(PacienteDTORequest pacienteDTORequest)
        {
            var response = await _pacienteRepository.EliminarPaciente(pacienteDTORequest);
            return response;
        }
    }
}
