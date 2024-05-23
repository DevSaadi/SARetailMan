using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositoryProject.Areas.Student.Data;
using RepositoryProject.Areas.Student.Models;
using RepositoryProject.Service.Interface;
using RepositoryProject.Utilities;
using System.Globalization;

namespace RepositoryProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Products()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            List<Product> productList = await _productService.List();
            List<VMProduct> vmProductList = productList.Select(MapToViewModel).ToList();
            return Ok(new { data = vmProductList });
        }

        private VMProduct MapToViewModel(Product product)
        {
            return new VMProduct
            {
                IdProduct = product.IdProduct,
                BarCode = product.BarCode,
                Brand = product.Brand,
                Description = product.Description,
                IdCategory = product.IdCategory,
                NameCategory = product.IdCategoryNavigation?.Description, 
                Quantity = product.Quantity,
                Price = product.Price?.ToString("F2"),
                Photo = product.Photo,
                PhotoBase64 = product.Photo != null ? Convert.ToBase64String(product.Photo) : null,
                IsActive = product.IsActive.HasValue ? (product.IsActive.Value ? 1 : 0) : (int?)null
            };


        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] IFormFile photo, [FromForm] string model)
        {
            GenericResponse<VMProduct> gResponse = new GenericResponse<VMProduct>();
            try
            {
                
                VMProduct vmProduct = JsonConvert.DeserializeObject<VMProduct>(model);

                if (photo != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        photo.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        vmProduct.Photo = fileBytes;
                    }
                }
                else
                {
                    vmProduct.Photo = null;
                }
                Product product = new Product
                {
                    IdProduct = vmProduct.IdProduct,
                    BarCode = vmProduct.BarCode,
                    Brand = vmProduct.Brand,
                    Description = vmProduct.Description,
                    IdCategory = vmProduct.IdCategory,
                    Quantity = vmProduct.Quantity,
                    Price = vmProduct.Price != null ? Convert.ToDecimal(vmProduct.Price, new CultureInfo("es-PE")) : (decimal?)null,
                    Photo = vmProduct.Photo,
                    IsActive = vmProduct.IsActive == 1
                };

                Product product_created = await _productService.Add(product);
                vmProduct = new VMProduct
                {
                    IdProduct = product_created.IdProduct,
                    BarCode = product_created.BarCode,
                    Brand = product_created.Brand,
                    Description = product_created.Description,
                    IdCategory = product_created.IdCategory,
                    NameCategory = product_created.IdCategoryNavigation?.Description,
                    Quantity = product_created.Quantity,
                    Price = product_created.Price.HasValue ? Convert.ToString(product_created.Price.Value, new CultureInfo("es-PE")) : null,
                    Photo = product_created.Photo,
                    PhotoBase64 = product_created.Photo != null ? Convert.ToBase64String(product_created.Photo) : null,
                    IsActive = product_created.IsActive.HasValue ? (product_created.IsActive.Value ? 1 : 0) : (int?)null
                };

                gResponse.State = true;
                gResponse.Object = vmProduct;
            }
            catch (Exception ex)
            {
                gResponse.State = false;
                gResponse.Message = ex.Message;
            }

            return Ok(gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> EditProduct([FromForm] IFormFile photo, [FromForm] string model)
        {
            GenericResponse<VMProduct> gResponse = new GenericResponse<VMProduct>();
            try
            {
                VMProduct vmProduct = JsonConvert.DeserializeObject<VMProduct>(model);

                if (photo != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        photo.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        vmProduct.Photo = fileBytes;
                    }
                }
                else
                {
                    vmProduct.Photo = null;
                }

                Product product = new Product
                {
                    IdProduct = vmProduct.IdProduct,
                    BarCode = vmProduct.BarCode,
                    Brand = vmProduct.Brand,
                    Description = vmProduct.Description,
                    IdCategory = vmProduct.IdCategory,
                    Quantity = vmProduct.Quantity,
                    Price = vmProduct.Price != null ? Convert.ToDecimal(vmProduct.Price, new CultureInfo("es-PE")) : (decimal?)null,
                    Photo = vmProduct.Photo,
                    IsActive = vmProduct.IsActive == 1
                };

                Product product_edited = await _productService.Edit(product);

                vmProduct = new VMProduct
                {
                    IdProduct = product_edited.IdProduct,
                    BarCode = product_edited.BarCode,
                    Brand = product_edited.Brand,
                    Description = product_edited.Description,
                    IdCategory = product_edited.IdCategory,
                    NameCategory = product_edited.IdCategoryNavigation?.Description,
                    Quantity = product_edited.Quantity,
                    Price = product_edited.Price.HasValue ? Convert.ToString(product_edited.Price.Value, new CultureInfo("es-PE")) : null,
                    Photo = product_edited.Photo,
                    PhotoBase64 = product_edited.Photo != null ? Convert.ToBase64String(product_edited.Photo) : null,
                    IsActive = product_edited.IsActive.HasValue ? (product_edited.IsActive.Value ? 1 : 0) : (int?)null
                };

                gResponse.State = true;
                gResponse.Object = vmProduct;
            }
            catch (Exception ex)
            {
                gResponse.State = false;
                gResponse.Message = ex.Message;
            }

            return Ok(gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int IdProduct)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                bool result = await _productService.Delete(IdProduct);
                gResponse.State = result;
                gResponse.Object = result ? "Product deleted successfully." : "Product deletion failed.";
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
