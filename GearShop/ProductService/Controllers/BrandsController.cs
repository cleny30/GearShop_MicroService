using BusinessObject.Models.Entity;
using Repository.IRepository;
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
