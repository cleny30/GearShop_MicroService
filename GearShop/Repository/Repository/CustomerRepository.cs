using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DataAccess.DAO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        public CustomerModel GetCustomerByUserName(string username) => CustomerDAO.GetCustomerByUserName(username);

        public Task<LoginAccountModel> LoginCustomer(string userLogin, string pass)
        => CustomerDAO.ClientLogin(userLogin, pass);

        public void UpdateCustomer(CustomerModel customerDTO) => CustomerDAO.UpdateCustomer(customerDTO);

        public Task<Customer> ChangePassword(ChangePasswordModel model) => CustomerDAO.ClientChangePassword(model);
    }
}
