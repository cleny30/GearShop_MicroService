using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DataAccess.Core.Constants;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeRepository _homeRepository;
        private readonly IProductRepository _proRepository;
        private readonly IBrandRepository _brandrepository;
        private ICategoryRepository _categoryRepository;
        public HomeController(IHomeRepository homeRepository, IProductRepository proRepository, IBrandRepository brandrepository, ICategoryRepository categoryRepository)
        {
            _homeRepository = homeRepository;
            _proRepository = proRepository;
            _brandrepository = brandrepository;
            _categoryRepository = categoryRepository; 
        }

        // GET: api/Home/SpecialSale
        /* [HttpGet("SpecialSale")]
         public ActionResult<List<ProductData>> GetSpecialSaleProducts()
         {
             var products = _homeRepository.GetSpecialSaleProducts();
             if (products == null || products.Count == 0)
             {
                 return NotFound("No special sale products found.");
             }
             return Ok(products);
         }*/
        // GET: api/Home/Mouse
        [HttpGet("Mouse")]
        public ActionResult<List<ProductData>> GetMouseProducts()
        {
            var products = _homeRepository.GetMouseProducts();
            if (products == null || products.Count == 0)
            {
                return NotFound("No mouse products found.");
            }
            return Ok(products);
        }
        // GET: api/Home/Keyboard
        [HttpGet("Keyboard")]
        public ActionResult<List<ProductData>> GetKeyboardProducts()
        {
            var products = _homeRepository.GetKeyboardProducts();
            if (products == null || products.Count == 0)
            {
                return NotFound("No keyboard products found.");
            }
            return Ok(products);
        }


        [HttpGet("GetHomeList")]
        public IActionResult GetProductList()
        {
            // Lấy danh sách sản phẩm và hình ảnh
            var list = _homeRepository.HomeGetData();
            var imgs = _proRepository.GetProductImages();

            if (list == null || !list.Any())
            {
                return NotFound();
            }

            AssignProductImages(list, imgs);

            var homeModel = new HomeModel
            {
                specialSale = GetSpecialSaleProducts(list),
                mouse = GetProductsByCategory(list, CategoryType.Mouse),
                keyboard = GetProductsByCategory(list, CategoryType.Keyboard),
                Brands = _brandrepository.GetBrands(),
                Categories = _categoryRepository.GetCategories(),
            };

            return Ok(homeModel);
        }
        private void AssignProductImages(IEnumerable<ProductData> products, IEnumerable<ProductImage> images)
        {
            foreach (var product in products)
            {
                product.ProImg = images
                    .Where(img => img.ProId == product.ProId)
                    .Select(img => img.ProImg)
                    .ToList();
            }
        }

        private List<ProductData> GetSpecialSaleProducts(IEnumerable<ProductData> products)
        {
            return products
                .Where(p => p.IsAvailable)
                .OrderByDescending(p => p.Discount)
                .Take(8)
                .ToList();
        }

        private List<ProductData> GetProductsByCategory(IEnumerable<ProductData> products, CategoryType category)
        {
            return products
                .Where(p => p.CateId == (int)category && p.IsAvailable)
                .Take(8)
                .ToList();
        }


    }
}
