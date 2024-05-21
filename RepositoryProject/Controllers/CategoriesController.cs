using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepositoryProject.Areas.Student.Data;
using RepositoryProject.Areas.Student.Models;
using RepositoryProject.Service.Interface;
using RepositoryProject.Utilities;

namespace RepositoryProject.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Categories()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            // Get the list of categories from the service
            List<Category> categories = await _categoryService.List();

            // Manually map the list of Category objects to a list of VMCategory objects
            List<VMCategory> vmCategoryList = categories.Select(category => new VMCategory
            {
                IdCategory = category.IdCategory,
                Description = category.Description,
                IsActive = category.IsActive == true ? 1 : 0,
            }).ToList();

            return Ok(new { data = vmCategoryList });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] VMCategory model)
        {
            GenericResponse<VMCategory> gResponse = new();
            try
            {
                // Manually map VMCategory to Category
                Category category = new()
                {
                    IdCategory = model.IdCategory,
                    Description = model.Description,
                    IsActive = model.IsActive == 1,
                };

                Category category_created = await _categoryService.Add(category);
                // Manually map Category back to VMCategory
                model = new VMCategory
                {
                    IdCategory = category_created.IdCategory,
                    Description = category_created.Description,
                    IsActive = category_created.IsActive == true ? 1 : 0,
                };

                gResponse.State = true;
                gResponse.Object = model;
            }
            catch (Exception ex)
            {
                gResponse.State = false;
                gResponse.Message = ex.Message;
            }

            return Ok(gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] VMCategory model)
        {
            GenericResponse<VMCategory> gResponse = new();
            try
            {
                // Manually map VMCategory to Category
                Category categoryToUpdate = new()
                {
                    IdCategory = model.IdCategory,
                    Description = model.Description,
                    IsActive = model.IsActive == 1,
                };

                Category edited_category = await _categoryService.Edit(categoryToUpdate);

                // Manually map Category back to VMCategory
                model = new VMCategory
                {
                    IdCategory = edited_category.IdCategory,
                    Description = edited_category.Description,
                    IsActive = edited_category.IsActive == true ? 1 : 0,
                };

                gResponse.State = true;
                gResponse.Object = model;
            }
            catch (Exception ex)
            {
                gResponse.State = false;
                gResponse.Message = ex.Message;
            }

            return Ok(gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int idCategory)
        {
            GenericResponse<string> gResponse = new();
            try
            {
                gResponse.State = await _categoryService.Delete(idCategory);
            }
            catch (Exception ex)
            {
                gResponse.State = false;
                gResponse.Message = ex.Message;
            }

            return Ok(gResponse);
        }


    }
}
