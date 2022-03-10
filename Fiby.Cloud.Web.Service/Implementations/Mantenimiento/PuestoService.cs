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
    public class PuestoService : IPuestoService
    {
        private readonly IPuestoRepository _puestoRepository;

        public PuestoService(IPuestoRepository puestoRepository)
        {
            _puestoRepository = puestoRepository;
        }

        public async Task<List<PuestoDTOResponse>> GetPuestoAll(PuestoDTORequest puestoDTORequest)
        {
            var response = await _puestoRepository.GetPuestoAll(puestoDTORequest);
            return response;
        }

        //public async Task<string> GuardarPuesto(PuestoDTORequest puestoDTORequest)
        //{
        //    var response = await _puestoRepository.GuardarPuesto(puestoDTORequest);
        //    return response;
        //}

        //public async Task<string> EditarPuesto(PuestoDTORequest puestoDTORequest)
        //{
        //    var response = await _puestoRepository.EditarPuesto(puestoDTORequest);
        //    return response;
        //}

        //public async Task<PuestoDTOResponse> GetPuestoPorId(PuestoDTORequest puestoDTORequest)
        //{
        //    var response = await _puestoRepository.GetPuestoPorId(puestoDTORequest);
        //    return response;
        //}
        //public async Task<string> EliminarPuesto(PuestoDTORequest puestoDTORequest)
        //{
        //    var response = await _puestoRepository.EliminarPuesto(puestoDTORequest);
        //    return response;
        //}
    }
}
