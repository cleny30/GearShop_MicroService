using BusinessObject.Models.Entity;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetAllProducts")]
        public ActionResult<List<Product>> GetAllProducts()
        {
            return _repository.GetProducts();
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
    }
}
