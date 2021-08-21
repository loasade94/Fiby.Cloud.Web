using Fiby.Cloud.Web.Client.App_Start.Extensions;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<List<CategoryDTOResponse>> GetCategoryAll(CategoryDTORequest categoryDTORequest)
        {
            categoryDTORequest.Description = categoryDTORequest.Description == null ? string.Empty : categoryDTORequest.Description;

            var model = await _categoryService.GetCategoryAll(categoryDTORequest);
            return model;
        }

        public async Task<IActionResult> RegisterUpdateCategory(int categoryId, int option)
        {
            CategoryDTOResponse categoryDTOResponse = new CategoryDTOResponse();
            CategoryDTORequest categoryDTORequest = new CategoryDTORequest()
            {
                CategoryId = categoryId
            };

            if (categoryId == 0) { ViewBag.titleModal = "Agregar"; }
            if (categoryId > 0 && option == 0) { ViewBag.titleModal = "Editar"; }

            ViewBag.listStatus = Lists.GetListStatus();

            if (categoryId > 0)//EDITAR
            {
                categoryDTOResponse = await _categoryService.GetCategoryById(categoryDTORequest);
                ViewBag.ModalGeneralIsNew = "0";
            }
            else
            {
                ViewBag.ModalGeneralIsNew = "1";
            }
            return PartialView(categoryDTOResponse);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterOrUpdateCategory(CategoryDTORequest request)
        {
            string response = string.Empty;
            request.Description = request.Description == null ? string.Empty : request.Description;

            if (request.CategoryId == 0)
            {
                response = await _categoryService.RegisterCategory(request);
            }
            else
            {
                response = await _categoryService.UpdateCategory(request);
            }

            return Json(response);
        }

        [HttpDelete]
        public async Task<string> DeleteCategory(int categoryId)
        {

            var categoryDTORequest = new CategoryDTORequest
            {
                CategoryId = categoryId,
            };

            string response = await _categoryService.DeleteCategory(categoryDTORequest);
            return response;
        }
    }
}
