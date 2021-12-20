using Fiby.Cloud.Web.DTO.Modules.Inversiones.Request;
using Fiby.Cloud.Web.DTO.Modules.Inversiones.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Inversiones;
using Fiby.Cloud.Web.Service.Interfaces.Inversiones;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Inversiones
{
    public class GastoService : IGastoService
    {
        private readonly IGastoRepository _gastoRepository;

        public GastoService(IGastoRepository gastoRepository)
        {
            _gastoRepository = gastoRepository;
        }

        public async Task<string> RegistrarGasto(GastoDTORequest gastoDTORequest)
        {
            var response = await _gastoRepository.RegistrarGasto(gastoDTORequest);
            return response;
        }

        public async Task<List<GastoDTOResponse>> GetGastoAll(GastoDTORequest gastoDTORequest)
        {
            var response = await _gastoRepository.GetGastoAll(gastoDTORequest);
            return response;
        }

        public async Task<string> EliminarGasto(GastoDTORequest gastoDTORequest)
        {
            var response = await _gastoRepository.EliminarGasto(gastoDTORequest);
            return response;
        }

        public async Task<GastoDTOResponse> GetGastoPorCodigo(GastoDTORequest gastoDTORequest)
        {
            var response = await _gastoRepository.GetGastoPorCodigo(gastoDTORequest);
            return response;
        }
    }
}
