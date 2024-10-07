using BusinessObject.DTOS;

namespace DataAccess.IRepository
{
    public interface ICustomerRepository
    {
        CustomerModel GetCustomerByUserName(string name);
        void UpdateCustomer(CustomerModel customer);

        public Task<LoginAccountModel> LoginCustomer(string userLogin, string pass);
    }
}
