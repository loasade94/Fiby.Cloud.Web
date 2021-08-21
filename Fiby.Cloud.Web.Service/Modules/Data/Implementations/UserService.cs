using AutoMapper;
using Fiby.Cloud.Cross.Util.DTOGeneric;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using Fiby.Cloud.Web.Persistence.Interfaces;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Modules.Data.Implementations
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClient _httpClient;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IHttpClient httpClient, IUserRepository userRepository, IMapper mapper)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<string> LoginUser(UserDTORequest userDTORequest)
        {
            string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/User/LoginUser";
            //string uri = $"{_configuration["proxy:UrlPersonal"]}/api/TableBase/GetTableDetailAll?tableCode={tableCode}";
            //string uri = ApiPaths.Personal.GetTableDetailAll(_isLocal, tableCode);
            var result = await _httpClient.GetStringAsync(uri, userDTORequest);
            var response = JsonConvert.DeserializeObject<ResponseObject<string>>(result);
            return response.Data;

        }

        public async Task<UserDTOResponse> GetUserLogin(UserDTORequest userDTORequest)
        {
            try
            {
                //string uri = ApiPaths.Security.PostAccountAuth();
                string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/User/GetUserLogin";
                var result = await _httpClient.PostInitAsync(uri, userDTORequest);
                result.EnsureSuccessStatusCode();
                var data = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponseObject<UserDTOResponse>>(data);

                if (response == null)
                    return null;
                else
                    return response.Data;
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        public async Task<string> LoginUserNew(UserDTORequest userDTORequest)
        {
            var response = await _userRepository.LoginUser(userDTORequest);
            return response;
        }

        public async Task<UserDTOResponse> GetUserLoginNew(UserDTORequest userDTORequest)
        {
            var response = await _userRepository.GetUserLogin(userDTORequest);
            return response;
        }
    }
}
