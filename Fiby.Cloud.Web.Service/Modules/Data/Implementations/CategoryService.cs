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
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClient _httpClient;
        public CategoryService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDTOResponse>> GetCategoryAll(CategoryDTORequest categoryDTORequest)
        {
            string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/Category/GetCategoryAll";
            //string uri = ApiPaths.Personal.GetTableDetailAll(_isLocal, tableCode);
            var result = await _httpClient.GetStringAsync(uri, categoryDTORequest);
            var response = JsonConvert.DeserializeObject<ResponseObject<List<CategoryDTOResponse>>>(result);
            return response.Data;
        }

        public async Task<string> RegisterCategory(CategoryDTORequest categoryDTORequest)
        {
            //string uri = ApiPaths.Security.PostAccountAuth();
            string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/Category/RegisterCategory";
            var result = await _httpClient.PostInitAsync(uri, categoryDTORequest);
            result.EnsureSuccessStatusCode();
            var data = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseObject<string>>(data);
            return response.Data;
        }

        public async Task<string> DeleteCategory(CategoryDTORequest categoryDTORequest)
        {
            //string uri = ApiPaths.Personal.DeleteApplyUpdate();
            string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/Category/DeleteCategory";
            var result = await _httpClient.DeleteAsync(uri, categoryDTORequest);
            var data = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseObject<string>>(data);
            return response.Data;
        }

        public async Task<string> UpdateCategory(CategoryDTORequest categoryDTORequest)
        {
            //string uri = ApiPaths.Security.PostAccountAuth();
            string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/Category/UpdateCategory";
            var result = await _httpClient.PostInitAsync(uri, categoryDTORequest);
            result.EnsureSuccessStatusCode();
            var data = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseObject<string>>(data);
            return response.Data;
        }
        public async Task<CategoryDTOResponse> GetCategoryById(CategoryDTORequest categoryDTORequest)
        {
            string uri = "http://loasadetestapi-001-site1.ftempurl.com/api/Category/GetCategoryById";
            var result = await _httpClient.GetStringAsync(uri, categoryDTORequest);
            var response = JsonConvert.DeserializeObject<ResponseObject<CategoryDTOResponse>>(result);
            return response.Data;
        }
    }
}
