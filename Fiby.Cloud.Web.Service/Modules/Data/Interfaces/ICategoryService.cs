using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Modules.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTOResponse>> GetCategoryAll(CategoryDTORequest categoryDTORequest);
        Task<string> RegisterCategory(CategoryDTORequest categoryDTORequest);
        Task<string> DeleteCategory(CategoryDTORequest categoryDTORequest);
        Task<string> UpdateCategory(CategoryDTORequest categoryDTORequest);
        Task<CategoryDTOResponse> GetCategoryById(CategoryDTORequest categoryDTORequest);
    }
}
