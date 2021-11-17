using AutoMapper;
using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.DTO.Modules.Horario.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Horario;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Horario
{
    public class SemanaService : ISemanaService
    {
        private readonly ISemanaRepository _semanaRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClient _httpClient;
        public SemanaService(IHttpClient httpClient, ISemanaRepository semanaRepository, IMapper mapper)
        {
            _httpClient = httpClient;
            _semanaRepository = semanaRepository;
            _mapper = mapper;
        }

        public async Task<List<SemanaDTOResponse>> GetDisponibilidadSemana(SemanaDTORequest semanaDTORequest)
        {
            var response = await _semanaRepository.GetDisponibilidadSemana(semanaDTORequest);
            return response;
        }

        public async Task<List<SemanaDTOResponse>> GetListaSemana()
        {
            var response = await _semanaRepository.GetListaSemana();
            return response;
        }

        public async Task<List<SemanaDTOResponse>> GetListaDiasXSemana(SemanaDTORequest semanaDTORequest)
        {
            var response = await _semanaRepository.GetListaDiasXSemana(semanaDTORequest);
            return response;
        }

        public async Task<List<SemanaDTOResponse>> GetListaHorario()
        {
            var response = await _semanaRepository.GetListaHorario();
            return response;
        }
    }
}
