using BusinessObject.DTOS;
using BusinessObject.Models.Entity;

namespace Repository.IRepository
{
    public interface IManagerRepository
    {
        public List<Manager> GetAll();

        public Task<Boolean> CheckUsernameExistedAsync(string username);

        public Task<Boolean> CheckManagerExistedAsync(string username, string password);

        public Task<ManagerModel> GetManagerByUsername(string username);
    }
}
