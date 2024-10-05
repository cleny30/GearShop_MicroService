using BusinessObject.DTOS;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;
        private IBrandRepository _brandRepository;
        private ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _brandRepository = brandRepository;
        }

        [HttpGet("GetProductListWithoutBrandAndCategory")]
        public async Task<IActionResult> GetProductListWithoutBrandAndCategory()
        {
            List<ProductModel> list = await _productRepository.GetProductListAdmin();
            return Ok(list);
        }

        [HttpGet("GetProductList")]
        public async Task<IActionResult> GetProductList()
        {
            List<ProductModel> productList = await _productRepository.GetProductListAdmin();
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
