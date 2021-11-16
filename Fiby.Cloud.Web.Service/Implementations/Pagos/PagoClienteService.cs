using AutoMapper;
using Fiby.Cloud.Web.DTO.Modules.Pagos.Request;
using Fiby.Cloud.Web.Persistence.Interfaces.Pagos;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Interfaces.Pagos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Pagos
{
    public class PagoClienteService : IPagoClienteService
    {
        private readonly IPagoClienteRepository _pagoClienteRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClient _httpClient;
        public PagoClienteService(IHttpClient httpClient, IPagoClienteRepository pagoClienteRepository, IMapper mapper)
        {
            _httpClient = httpClient;
            _pagoClienteRepository = pagoClienteRepository;
            _mapper = mapper;
        }
        public async Task<string> ActualizarPagoClienteXServicio(PagoClienteDTORequest pagoClienteDTORequest)
        {
            var response = await _pagoClienteRepository.ActualizarPagoClienteXServicio(pagoClienteDTORequest);
            return response;
        }
    }
}
