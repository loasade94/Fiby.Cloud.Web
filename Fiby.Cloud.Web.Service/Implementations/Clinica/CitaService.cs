using Fiby.Cloud.Web.DTO.Modules.Clinica.Request;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Clinica;
using Fiby.Cloud.Web.Service.Interfaces.Clinica;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Clinica
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

    }
}
