
using BusinessObject.Model.Entity;

namespace Repository.IRepository
{
    public interface IAddressRepository
    {
        public bool AddNewAddress(DeliveryAddressModel deliAddressModel, string username);
        public List<DeliveryAddressModel> GetAllAddress(string username);
        public bool UpdateAddress(DeliveryAddressModel deliveryAddressModel);
        public bool DeleteAddress(string username, int id);
        public DeliveryAddressModel? FindExistingAddressItem(string username, string phoneNumber, string fullname, string address, bool isdefault);
        public void CheckAllFalse(string username);
    }
}
