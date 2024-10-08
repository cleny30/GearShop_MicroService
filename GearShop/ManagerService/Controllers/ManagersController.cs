using BusinessObject.Models.Entity;
using Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ManagerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {

        private IManagerRepository _repository;

        public ManagersController(IManagerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Manager>> GetAll()
        {
            return _repository.GetAll();
        }


        [HttpGet("CheckUsernameExisted")]
        public async Task<ActionResult> CheckUsernameExisted(string username)
        {
            bool exists = await _repository.CheckUsernameExistedAsync(username);
            return Ok(exists);
        }

        [HttpGet("CheckManagerExisted")]
        public async Task<ActionResult> CheckManagerExisted(string username, string password)
        {
            bool exists = await _repository.CheckManagerExistedAsync(username, password);
            return Ok(exists);
        }

    }
}
