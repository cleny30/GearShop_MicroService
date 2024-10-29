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
        public Task<bool> CheckMail(string mail)
       => CustomerDAO.CheckMail(mail);

        public void ForgetPassword(string mail, string pass)
        => CustomerDAO.ForgetPassword(mail, pass);

        public Task<bool> CheckUsername(string username)
        => CustomerDAO.CheckUsername(username);

        public Task<bool> Register(RegisterModel userRegist)
        => CustomerDAO.Register(userRegist);
    }
}
