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
        #region AutoMaplessCode
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
                //RegistrationDate = category.RegistrationDate
            }).ToList();

            // Return the result with status code 200 (OK)
            return Ok(new { data = vmCategoryList });
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] VMCategory model)
        {
            GenericResponse<VMCategory> gResponse = new GenericResponse<VMCategory>();
            try
            {
                // Manually map VMCategory to Category
                Category category = new Category
                {
                    IdCategory = model.IdCategory,
                    Description = model.Description,
                    IsActive = model.IsActive == 1,
                    //RegistrationDate = model.RegistrationDate
                };

                // Add the category using the service
                Category category_created = await _categoryService.Add(category);

                // Manually map Category back to VMCategory
                model = new VMCategory
                {
                    IdCategory = category_created.IdCategory,
                    Description = category_created.Description,
                    IsActive = category_created.IsActive == true ? 1 : 0,
                    //RegistrationDate = category_created.RegistrationDate
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
        #endregion

        //[HttpGet]
        //public async Task<IActionResult> GetCategories()
        //{

        //    List<VMCategory> vmCategoryList = _mapper.Map<List<VMCategory>>(await _categoryService.List());
        //    return Ok(new { data = vmCategoryList });
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateCategory([FromBody] VMCategory model)
        //{
        //    GenericResponse<VMCategory> gResponse = new GenericResponse<VMCategory>();
        //    try
        //    {
        //        Category category_created = await _categoryService.Add(_mapper.Map<Category>(model));

        //        model = _mapper.Map<VMCategory>(category_created);

        //        gResponse.State = true;
        //        gResponse.Object = model;
        //    }
        //    catch (Exception ex)
        //    {
        //        gResponse.State = false;
        //        gResponse.Message = ex.Message;
        //    }

        //    return Ok(gResponse);
        //}
    }
}
