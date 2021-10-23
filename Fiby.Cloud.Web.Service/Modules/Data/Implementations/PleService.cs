using AutoMapper;
using Fiby.Cloud.Cross.Util.DTOGeneric;
using Fiby.Cloud.Web.DTO.Modules.Ple.Request;
using Fiby.Cloud.Web.DTO.Modules.Ple.Response;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using Fiby.Cloud.Web.Persistence.Interfaces;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Modules.Data.Implementations
{
    public class PleService : IPleService
    {
        private readonly IPleRepository _pleRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClient _httpClient;
        public PleService(IHttpClient httpClient, IPleRepository pleRepository, IMapper mapper)
        {
            _httpClient = httpClient;
            _pleRepository = pleRepository;
            _mapper = mapper;
        }

        public async Task<List<PleDTOResponse>> GetPleAll(PleDTORequest pleDTORequest)
        {
            string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/Ple/GetPleAll";
            //string uri = ApiPaths.Personal.GetTableDetailAll(_isLocal, tableCode);
            var result = await _httpClient.GetStringAsync(uri, pleDTORequest);
            var response = JsonConvert.DeserializeObject<ResponseObject<List<PleDTOResponse>>>(result);
            return response.Data;
        }

        public async Task<List<PleDTOResponse>> GetPleAllNew(PLE14100DTORequest pLE14100DTORequest)
        {
            var response = await _pleRepository.GetPleAll(pLE14100DTORequest);
            return response;
        }

        public async Task<string> RegistrarPle14100DPorMes(PLE14100DTORequest pLE14100DTORequest)
        {
            var response = await _pleRepository.RegistrarPle14100DPorMes(pLE14100DTORequest);
            return response;
        }

        public async Task<List<PLE14100DTOResponse>> GetPlePLE14100All(PLE14100DTORequest pLE14100DTORequest)
        {
            var response = await _pleRepository.GetPlePLE14100All(pLE14100DTORequest);
            return response;
        }

    }
}
