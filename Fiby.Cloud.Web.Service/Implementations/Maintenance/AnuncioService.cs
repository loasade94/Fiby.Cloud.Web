using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Maintenance;
using Fiby.Cloud.Web.Service.Interfaces.Maintenance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Maintenance
{
    public class AnuncioService : IAnuncioService
    {
        private readonly IAnuncioRepository _anuncioRepository;

        public AnuncioService(IAnuncioRepository anuncioRepository)
        {
            _anuncioRepository = anuncioRepository;
        }

        public async Task<string> RegistrarAnuncio(AnuncioDTORequest gastoDTORequest)
        {
            var response = await _anuncioRepository.RegistrarAnuncio(gastoDTORequest);
            return response;
        }

        public async Task<List<AnuncioDTOResponse>> GetAnuncioAll(AnuncioDTORequest gastoDTORequest)
        {
            var response = await _anuncioRepository.GetAnuncioAll(gastoDTORequest);
            return response;
        }

        public async Task<string> EliminarAnuncio(AnuncioDTORequest gastoDTORequest)
        {
            var response = await _anuncioRepository.EliminarAnuncio(gastoDTORequest);
            return response;
        }

        public async Task<AnuncioDTOResponse> GetAnuncioPorCodigo(AnuncioDTORequest gastoDTORequest)
        {
            var response = await _anuncioRepository.GetAnuncioPorCodigo(gastoDTORequest);
            return response;
        }
    }
}
