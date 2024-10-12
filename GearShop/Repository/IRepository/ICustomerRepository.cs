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
    }
}
