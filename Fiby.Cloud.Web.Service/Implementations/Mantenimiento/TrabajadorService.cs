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
    public class TrabajadorService : ITrabajadorService
    {
        private readonly ITrabajadorRepository _trabajadorRepository;

        public TrabajadorService(ITrabajadorRepository trabajadorRepository)
        {
            _trabajadorRepository = trabajadorRepository;
        }

        public async Task<List<TrabajadorDTOResponse>> GetTrabajadorAll(TrabajadorDTORequest trabajadorDTORequest)
        {
            var response = await _trabajadorRepository.GetTrabajadorAll(trabajadorDTORequest);
            return response;
        }

        public async Task<string> GuardarTrabajador(TrabajadorDTORequest trabajadorDTORequest)
        {
            var response = await _trabajadorRepository.GuardarTrabajador(trabajadorDTORequest);
            return response;
        }

        public async Task<string> EditarTrabajador(TrabajadorDTORequest trabajadorDTORequest)
        {
            var response = await _trabajadorRepository.EditarTrabajador(trabajadorDTORequest);
            return response;
        }

        public async Task<TrabajadorDTOResponse> GetTrabajadorPorId(TrabajadorDTORequest trabajadorDTORequest)
        {
            var response = await _trabajadorRepository.GetTrabajadorPorId(trabajadorDTORequest);
            return response;
        }
        public async Task<string> EliminarTrabajador(TrabajadorDTORequest trabajadorDTORequest)
        {
            var response = await _trabajadorRepository.EliminarTrabajador(trabajadorDTORequest);
            return response;
        }
    }
}
