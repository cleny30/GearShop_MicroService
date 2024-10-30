using BusinessObject.DTOS;
using Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Models.Entity;

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

        [HttpPost("AddImportReceipt")]
        public async Task<IActionResult> AddImportReceipt(ImportProductModel _importProductModel)
        {
            ImportProductModel importProduct = await _repository.CreateImportReceipt(_importProductModel);
            return Ok(importProduct);
        }

        [HttpPost("AddImportProduct")]
        public async Task<IActionResult> AddImportProduct(List<ReceiptProductModel> list)
        {
            var isSuccess = await _repository.AddReceiptProduct(list);
            return Ok(isSuccess); 
        }

        [HttpGet("GetImportProductByID")]
        public async Task<IActionResult> GetImportProductByID(int ImportProductId)
        {
            ImportProductModel model = await _repository.GetImportProduct(ImportProductId);
            return Ok(model);
        }

        [HttpGet("GetReceiptProductByID")]
        public async Task<IActionResult> GetReceiptProductByID(int ImportProductId)
        {
            List<ReceiptProductModel> list = await _repository.GetReceiptProductsByID(ImportProductId);
            return Ok(list);
        }
    }
}
