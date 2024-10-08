using BusinessObject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IManagerRepository
    {
        public List<Manager> GetAll();

        public Task<Boolean> CheckUsernameExistedAsync(string username);

        public Task<Boolean> CheckManagerExistedAsync(string username, string password);
    }
}
