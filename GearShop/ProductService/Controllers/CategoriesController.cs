using BusinessObject.Models.Entity;
using Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Category>> GetAllCategories()
        {
            return _repository.GetCategories();
        }
    }
}
