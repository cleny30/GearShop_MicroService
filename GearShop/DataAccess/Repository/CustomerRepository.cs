using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

       

        public CustomerModel GetCustomerByName(string name)
       => CustomerDAO.GetCustomerByName(name);
        public void UpdateCustomer(CustomerModel customerDTO)
            => CustomerDAO.UpdateCustomer(customerDTO);


    }
}
