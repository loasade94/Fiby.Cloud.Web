﻿using Fiby.Cloud.Cross.Util.DTOGeneric;
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
            string uri = "https://localhost:5032/api/Rol/GetRolAll";
            //string uri = $"{_configuration["proxy:UrlPersonal"]}/api/TableBase/GetTableDetailAll?tableCode={tableCode}";
            //string uri = ApiPaths.Personal.GetTableDetailAll(_isLocal, tableCode);
            var result = await _httpClient.GetStringAsync(uri, rolDTORequest);
            var response = JsonConvert.DeserializeObject<ResponseObject<List<RolDTOResponse>>>(result);
            return response.Data;
        }

        public async Task<string> RegisterRol(RolDTORequest rolDTORequest)
        {
            //string uri = ApiPaths.Security.PostAccountAuth();
            string uri = "https://localhost:5032/api/Rol/RegisterRol";
            var result = await _httpClient.PostInitAsync(uri, rolDTORequest);
            result.EnsureSuccessStatusCode();
            var data = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseObject<string>>(data);
            return response.Data;
        }

    }
}
