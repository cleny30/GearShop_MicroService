using BusinessObject.Models.Entity;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
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

    }
}
