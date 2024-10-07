using BusinessObject.DTOS;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        public CustomerModel GetCustomerByUserName(string username) => CustomerDAO.GetCustomerByUserName(username);

        public Task<LoginAccountModel> LoginCustomer(string userLogin, string pass)
        => CustomerDAO.ClientLogin(userLogin, pass);

        public void UpdateCustomer(CustomerModel customerDTO) => CustomerDAO.UpdateCustomer(customerDTO);
    }
}
