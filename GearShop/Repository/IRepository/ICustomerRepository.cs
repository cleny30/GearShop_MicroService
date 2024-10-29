using BusinessObject.DTOS;
using BusinessObject.Models.Entity;

namespace Repository.IRepository
{
    public interface ICustomerRepository
    {
        CustomerModel GetCustomerByUserName(string name);
        void UpdateCustomer(CustomerModel customer);

        public Task<LoginAccountModel> LoginCustomer(string userLogin, string pass);
        public Task<Customer> ChangePassword(ChangePasswordModel model);
        public Task<bool> CheckMail(string mail);  
        public Task<bool> CheckUsername(string username);
        public Task<bool> Register(RegisterModel userRegist);

        public void ForgetPassword(string mail, string pass);

    }
}
