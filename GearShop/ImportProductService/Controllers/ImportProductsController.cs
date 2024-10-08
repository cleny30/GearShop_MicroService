using BusinessObject.DTOS;
using Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ImportProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportProductsController : ControllerBase
    {
        private IImportProductRepository _repository;

        public ImportProductsController(IImportProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetImportProductList")]
        public async Task<ActionResult<List<ImportProductModel>>> GetImportProductsList()
        {
            return await _repository.GetImportProductsList();
        }

        [HttpGet("GetMoneySpent")]
        public async Task<double> GetMoneySpent()
        {
            return await _repository.GetMoneySpent();
        }
    }
}
