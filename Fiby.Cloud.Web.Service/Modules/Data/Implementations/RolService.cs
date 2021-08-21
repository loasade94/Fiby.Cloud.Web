using Fiby.Cloud.Cross.Util.DTOGeneric;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Modules.Data.Implementations
{
    public class RolService : IRolService
    {

        private readonly IHttpClient _httpClient;
        public RolService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RolDTOResponse>> GetRolAll(RolDTORequest rolDTORequest)
        {
            string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/Rol/GetRolAll";
            //string uri = $"{_configuration["proxy:UrlPersonal"]}/api/TableBase/GetTableDetailAll?tableCode={tableCode}";
            //string uri = ApiPaths.Personal.GetTableDetailAll(_isLocal, tableCode);
            var result = await _httpClient.GetStringAsync(uri, rolDTORequest);
            var response = JsonConvert.DeserializeObject<ResponseObject<List<RolDTOResponse>>>(result);
            return response.Data;
        }

        public async Task<string> RegisterRol(RolDTORequest rolDTORequest)
        {
            //string uri = ApiPaths.Security.PostAccountAuth();
            string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/Rol/RegisterRol";
            var result = await _httpClient.PostInitAsync(uri, rolDTORequest);
            result.EnsureSuccessStatusCode();
            var data = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseObject<string>>(data);
            return response.Data;
        }

        public async Task<string> DeleteRol(RolDTORequest rolDTORequest)
        {
            //string uri = ApiPaths.Personal.DeleteApplyUpdate();
            string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/Rol/DeleteRol";
            var result = await _httpClient.DeleteAsync(uri, rolDTORequest);
            var data = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseObject<string>>(data);
            return response.Data;
        }

        public async Task<RolDTOResponse> GetRolById(RolDTORequest rolDTORequest)
        {
            string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/Rol/GetRolById";
            var result = await _httpClient.GetStringAsync(uri, rolDTORequest);
            var response = JsonConvert.DeserializeObject<ResponseObject<RolDTOResponse>>(result);
            return response.Data;
        }

        public async Task<string> UpdateRol(RolDTORequest rolDTORequest)
        {
            //string uri = ApiPaths.Security.PostAccountAuth();
            string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/Rol/UpdateRol";
            var result = await _httpClient.PostInitAsync(uri, rolDTORequest);
            result.EnsureSuccessStatusCode();
            var data = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseObject<string>>(data);
            return response.Data;
        }

    }
}
