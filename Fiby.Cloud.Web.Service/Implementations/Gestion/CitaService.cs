using Fiby.Cloud.Web.DTO.Modules.Gestion.Request;
using Fiby.Cloud.Web.DTO.Modules.Gestion.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Gestion;
using Fiby.Cloud.Web.Service.Interfaces.Gestion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Gestion
{
    public class CitaService : ICitaService
    {
        private readonly ICitaRepository _citaRepository;

        public CitaService(ICitaRepository CitaRepository)
        {
            _citaRepository = CitaRepository;
        }
        public async Task<List<CitaDTOResponse>> GetCitaAll(CitaDTORequest citaDTORequest)
        {
            var response = await _citaRepository.GetCitaAll(citaDTORequest);
            return response;
        }

        public async Task<string> GuardarCita(CitaDTORequest citaDTORequest)
        {
            var response = await _citaRepository.GuardarCita(citaDTORequest);
            return response;
        }

        public async Task<string> EditarCita(CitaDTORequest citaDTORequest)
        {
            var response = await _citaRepository.EditarCita(citaDTORequest);
            return response;
        }

        public async Task<CitaDTOResponse> GetCitaPorId(CitaDTORequest citaDTORequest)
        {
            var response = await _citaRepository.GetCitaPorId(citaDTORequest);
            return response;
        }
        public async Task<string> EliminarCita(CitaDTORequest citaDTORequest)
        {
            var response = await _citaRepository.EliminarCita(citaDTORequest);
            return response;
        }

    }
}
