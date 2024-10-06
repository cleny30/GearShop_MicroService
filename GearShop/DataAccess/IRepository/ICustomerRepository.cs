using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface ICustomerRepository
    {
        CustomerModel GetCustomerByName(string name);
        void UpdateCustomer(CustomerModel customer);
    }
}
