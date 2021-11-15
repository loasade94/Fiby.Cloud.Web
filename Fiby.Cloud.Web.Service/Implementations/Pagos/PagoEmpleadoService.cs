using AutoMapper;
using Fiby.Cloud.Web.DTO.Modules.Pagos.Request;
using Fiby.Cloud.Web.DTO.Modules.Pagos.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Pagos;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Interfaces.Pagos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Pagos
{
    public class PagoEmpleadoService : IPagoEmpleadoService
    {
        private readonly IPagoEmpleadoRepository _pagoEmpleadoRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClient _httpClient;
        public PagoEmpleadoService(IHttpClient httpClient, IPagoEmpleadoRepository pagoEmpleadoRepository, IMapper mapper)
        {
            _httpClient = httpClient;
            _pagoEmpleadoRepository = pagoEmpleadoRepository;
            _mapper = mapper;
        }

        public async Task<List<PagoEmpleadoDTOResponse>> GetPagosXEmpleadoSemana(PagoEmpleadoDTORequest pagoEmpleadoDTORequest)
        {
            var response = await _pagoEmpleadoRepository.GetPagosXEmpleadoSemana(pagoEmpleadoDTORequest);
            return response;
        }

        public async Task<string> RegistrarPagoEmpleado(PagoEmpleadoDTORequest pagoEmpleadoDTORequest)
        {
            var response = await _pagoEmpleadoRepository.RegistrarPagoEmpleado(pagoEmpleadoDTORequest);
            return response;
        }

        public async Task<List<PagoEmpleadoDTOResponse>> GetPagosXEmpleadoSemanaCab(PagoEmpleadoDTORequest pagoEmpleadoDTORequest)
        {
            var response = await _pagoEmpleadoRepository.GetPagosXEmpleadoSemanaCab(pagoEmpleadoDTORequest);
            return response;
        }

        public async Task<string> ActualizarPasajeXServicio(PagoEmpleadoDTORequest pagoEmpleadoDTORequest)
        {
            var response = await _pagoEmpleadoRepository.ActualizarPasajeXServicio(pagoEmpleadoDTORequest);
            return response;
        }
    }
}
