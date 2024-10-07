using BusinessObject.DTOS;

namespace DataAccess.IRepository
{
    public interface ICustomerRepository
    {
        CustomerModel GetCustomerByUserName(string name);
        void UpdateCustomer(CustomerModel customer);
    }
}
