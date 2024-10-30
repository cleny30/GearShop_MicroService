using BusinessObject.Models.Entity;
using Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.DTOS;

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

        [HttpPut("ChangeBrandStatus")]
        public async Task<IActionResult> ChangeBrandStatus(int BrandId, bool Status)
        {
            bool isSuccessful = await _repository.ChangeBrandStatus(BrandId, Status);
            return Ok(isSuccessful);
        }

        [HttpPost("InsertNewBrand")]
        public async Task<IActionResult> InsertNewBrand(InsertBrandModel brand)
        {
            bool isSuccessful = await _repository.InsertNewBrand(brand);
            return Ok(isSuccessful);
        }

        [HttpPut("UpdateBrand")]
        public async Task<IActionResult> UpdateBrand(UpdateBrandModel brand)
        {
            bool isSuccessful = await _repository.UpdateBrand(brand);
            return Ok(isSuccessful);
        }
    }
}
