using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private IManagerRepository _repository;

        public ManagerController(IManagerRepository repository)
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
