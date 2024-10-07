using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
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
        private IProductImageRepository _productImageRepository;
        private IProductAttributeRepository _productAttributeRepository;
        public ProductsController(IProductRepository repository, IBrandRepository brandRepository, ICategoryRepository categoryRepository,
            IProductAttributeRepository productAttributeRepository, IProductImageRepository productImageRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _productImageRepository = productImageRepository;
            _productAttributeRepository = productAttributeRepository;
        }

        [HttpGet("GetAllProducts")]
        public ActionResult<List<ProductData>> GetAllProducts()
        {
            SubService.ProductService _service = new SubService.ProductService();
            List<Product> list = _repository.GetProducts();
            List<ProductImage> imgs = _repository.GetProductImages();
            List<ProductAttribute> atts = _repository.GetProductAttributes();

            return _service.GetProducts(list, imgs, atts);
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

        [HttpGet("GetProductByName/{name}")]
        public async Task<ActionResult<IEnumerable<ProductData>>> GetProductsByName(string name)
        {

            List<ProductData> result = await _repository.SearchProductsByName(name);
            List<ProductImage> imgs = _repository.GetProductImages();

            foreach (ProductData pro in result)
            {
                pro.ProImg = imgs.Where(img => img.ProId == pro.ProId).Select(img => img.ProImg).ToList();
            }
            return result;
        }

        [HttpGet("GetNewProductID/{CatID}")]
        public async Task<IActionResult> GetNewProductID(int CatID)
        {
            string ProductID = await _repository.GetNewProductID(CatID);
            return Ok(ProductID);
        }

        [HttpPost("InsertProduct")]
        public async Task<IActionResult> InsertProduct(ProductData product)
        {
            bool isSuccess = await _repository.InsertProduct(product);
            return Ok(isSuccess);
        }

        [HttpPost("InsertProductImage")]
        public async Task<IActionResult> InsertImageOfProduct(List<ProductImageModel> imageList)
        {
            bool isSuccess = await _productImageRepository.AddImageOfSpecificProduct(imageList);
            return Ok(isSuccess);
        }

        [HttpPost("InsertProductAttribute")]
        public async Task<IActionResult> InsertAttributeOfProduct(List<ProductAttributeModel> productAttributes)
        {
            bool isSuccess = await _productAttributeRepository.AddProductAttribute(productAttributes);
            return Ok(isSuccess);
        }

        [HttpGet("GetProductByID/{ProId}")]
        public async Task<IActionResult> GetProductByID(string ProId)
        {
            ProductModel product = await _repository.GetProductByID(ProId);
            return Ok(product); 
        }

        [HttpGet("GetProductImageByID/{ProId}")]
        public async Task<IActionResult> GetProductImageByID(string ProId)
        {
            List<ProductImageModel> list = await _productImageRepository.GetProductImagesByID(ProId);
            return Ok(list);
        }

        [HttpGet("GetProductAttributeByID/{ProId}")]
        public async Task<IActionResult> GetProductAttributeByID(string ProId)
        {
            List<ProductAttributeModel> list = await _productAttributeRepository.GetProductAttributesByID(ProId);
            return Ok(list);
        }
    }
}
