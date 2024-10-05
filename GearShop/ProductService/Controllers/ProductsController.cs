using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using ProductService.SubService;
using System.Collections.Generic;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _repository;
        private IBrandRepository _brandRepository;
        private ICategoryRepository _categoryRepository;
        public ProductsController(IProductRepository repository, IBrandRepository brandRepository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }

        [HttpGet("GetAllProducts")]
        public ActionResult<List<ProductData>> GetAllProducts()
        {
            SubService.ProductService _service = new SubService.ProductService();
            List<Product> list = _repository.GetProducts();
            List<ProductImage> imgs = _repository.GetProductImages();
            List<ProductAttribute> atts = _repository.GetProductAttributes();

            return _service.GetProducts(list,imgs,atts);
        }

        [HttpGet("GetProductListWithoutBrandAndCategory")]
        public async Task<IActionResult> GetProductListWithoutBrandAndCategory()
        {
            List<ProductModel> list = await _repository.GetProductListAdmin();
            return Ok(list);
        }

        [HttpGet("GetProductImages")]
        public ActionResult<List<ProductImage>> GetProductImages()
        {
            return _repository.GetProductImages();
        }

        [HttpGet("GetProductAttributes")]
        public ActionResult<List<ProductAttribute>> GetProductAttributes()
        {
            return _repository.GetProductAttributes();
        }

        [HttpGet("GetProductList")]
        public async Task<IActionResult> GetProductList()
        {
            List<ProductModel> productList = await _repository.GetProductListAdmin();
            List<BrandModel> brandList = await _brandRepository.GetBrandList();
            List<CategoryModel> categoriesList = await _categoryRepository.GetCategoryList();

            foreach (ProductModel items in productList)
            {
                items.BrandName = brandList.FirstOrDefault(b => b.BrandId == items.BrandId).BrandName;
                items.CateName = categoriesList.FirstOrDefault(b => b.CateId == items.CateId).CateName;
            }

            return Ok(productList);
        }
    }
}
