using BusinessObject.DTOS;

namespace Repository.IRepository
{
    public interface ICustomerRepository
    {
        CustomerModel GetCustomerByUserName(string name);
        void UpdateCustomer(CustomerModel customer);

        public Task<LoginAccountModel> LoginCustomer(string userLogin, string pass);
    }
}
