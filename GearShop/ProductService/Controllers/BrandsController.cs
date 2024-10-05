using BusinessObject.Models.Entity;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private IBrandRepository _repository;

        public BrandsController(IBrandRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Brand>> GetAllBrands()
        {
            return _repository.GetBrands();
        }
    }
}
