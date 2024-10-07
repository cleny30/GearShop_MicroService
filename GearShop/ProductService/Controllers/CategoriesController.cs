using BusinessObject.Models.Entity;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
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
        public ActionResult<List<Category>> GetHomeCategories()
        {
            return _repository.GetCategories();
        }
    }
}
