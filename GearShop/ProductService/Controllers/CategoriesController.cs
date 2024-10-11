using BusinessObject.Models.Entity;
using Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.DTOS;

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

        [HttpGet("IsKeywordExisted")]
        public async Task<IActionResult> IsKeywordExisted(string keyword)
        {
            bool isSuccessful = await _repository.IsKeywordExisted(keyword);
            return Ok(isSuccessful);
        }

        [HttpPut("ChangeCategoryStatus")]
        public async Task<IActionResult> ChangeCategoryStatus(int CateId, bool Status)
        {
            bool isSuccessful = await _repository.ChangeCategoryStatus(CateId, Status);
            return Ok(isSuccessful);
        }

        [HttpPost("InsertNewCategory")]
        public async Task<IActionResult> InsertNewCategory(InsertCategoryModel category)
        {
            bool isSuccessful = await _repository.InsertNewCategory(category);
            return Ok(isSuccessful);
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryModel category)
        {
            bool isSuccessful = await _repository.UpdateCategory(category);
            return Ok(isSuccessful);
        }
    }
}
