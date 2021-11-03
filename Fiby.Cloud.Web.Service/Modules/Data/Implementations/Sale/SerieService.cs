using AutoMapper;
using Fiby.Cloud.Web.DTO.Modules.Sale.Request.Serie;
using Fiby.Cloud.Web.DTO.Modules.Sale.Response.Serie;
using Fiby.Cloud.Web.Persistence.Interfaces;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces.Sale;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Modules.Data.Implementations.Sale
{
    public class SerieService : ISerieService
    {

        private readonly ISerieRepository _serieRepository;
        private readonly IMapper _mapper;

        public SerieService(ISerieRepository rolRepository, IMapper mapper)
        {
            _serieRepository = rolRepository;
            _mapper = mapper;
        }

        public async Task<List<SerieDTOResponse>> GetSerieAll(SerieDTORequest serieDTORequest)
        {
            var response = await _serieRepository.GetSerieAll(serieDTORequest);
            return response;
        }

        public async Task<string> RegisterSerie(SerieDTORequest serieDTORequest)
        {
            var response = await _serieRepository.RegisterSerie(serieDTORequest);
            return response;
        }

        public async Task<string> DeleteSerie(SerieDTORequest serieDTORequest)
{
            var response = await _serieRepository.DeleteSerie(serieDTORequest);
            return response;
        }

        public async Task<string>  UpdateSerie(SerieDTORequest serieDTORequest)
{
            var response = await _serieRepository.UpdateSerie(serieDTORequest);
            return response;
        }

        public async Task<SerieDTOResponse> GetSerieById(SerieDTORequest serieDTORequest)
{
            var response = await _serieRepository.GetSerieById(serieDTORequest);
            return response;
        }
    }
}
