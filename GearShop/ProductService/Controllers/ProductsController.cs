using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DataAccess.IRepository;
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

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
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
