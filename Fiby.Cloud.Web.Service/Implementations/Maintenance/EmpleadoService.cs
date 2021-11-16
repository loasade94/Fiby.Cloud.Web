using AutoMapper;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Request;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Maintenance;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClient _httpClient;
        public EmpleadoService(IHttpClient httpClient, IEmpleadoRepository empleadoRepository, IMapper mapper)
        {
            _httpClient = httpClient;
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpleadoDTOResponse>> GetEmpleadoAll()
        {
            var response = await _empleadoRepository.GetEmpleadoAll();
            return response;
        }
        //public async Task<List<EmpleadoDTOResponse>> GetEmpleadoAll()
        //{
        //    var response = await _empleadoRepository.GetEmpleadoApellido;
        //    return response;
        //}
    }
}
