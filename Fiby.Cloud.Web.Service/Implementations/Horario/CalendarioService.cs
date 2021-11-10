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
    public class CalendarioService : ICalendarioService
    {

        private readonly ICalendarioRepository _calendarioRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClient _httpClient;
        public CalendarioService(IHttpClient httpClient, ICalendarioRepository calendarioRepository, IMapper mapper)
        {
            _httpClient = httpClient;
            _calendarioRepository = calendarioRepository;
            _mapper = mapper;
        }

        public async Task<string> RegistrarServicio(CalendarioDTORequest calendarioDTORequest)
        {
            var response = await _calendarioRepository.RegistrarServicio(calendarioDTORequest);
            return response;
        }
        public async Task<List<CalendarioDTOResponse>> GetServicioXEmpleado(CalendarioDTORequest calendarioDTORequest)
        {
            var response = await _calendarioRepository.GetServicioXEmpleado(calendarioDTORequest);
            return response;
        }
        public async Task<List<CalendarioDTOResponse>> GetServicioXEmpleadoCalendario(CalendarioDTORequest calendarioDTORequest)
        {
            var response = await _calendarioRepository.GetServicioXEmpleadoCalendario(calendarioDTORequest);
            return response;
        }
        public async Task<List<CalendarioDTOResponse>> GetServicioXEmpleadoTotales(CalendarioDTORequest calendarioDTORequest)
        {
            var response = await _calendarioRepository.GetServicioXEmpleadoTotales(calendarioDTORequest);
            return response;
        }
        public async Task<string> EliminarServicio(CalendarioDTORequest calendarioDTORequest)
        {
            var response = await _calendarioRepository.EliminarServicio(calendarioDTORequest);
            return response;
        }
        public async Task<CalendarioDTOResponse> GetCalendarioById(CalendarioDTORequest calendarioDTORequest)
        {
            var response = await _calendarioRepository.GetCalendarioById(calendarioDTORequest);
            return response;
        }
    }
}
