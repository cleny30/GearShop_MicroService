using BusinessObject.Models.Entity;
using DataAccess.DAO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        public async Task<bool> CheckManagerExistedAsync(string username, string password)
        => await ManagerDAO.CheckManagerExistedAsync(username, password);

        public async Task<bool> CheckUsernameExistedAsync(string username)
        => await ManagerDAO.CheckUsernameExistedAsync(username);

        public List<Manager> GetAll() => ManagerDAO.GetManagers();
    }
}
