
using BusinessObject.Model.Entity;

namespace Repository.IRepository
{
    public interface IAddressRepository
    {
      /*  public bool AddNewAddress(DeliveryAddressModel deliAddressModel);*/
        public List<DeliveryAddressModel> GetAllAddress(string username);
        public bool AddNewAddress(DeliveryAddressModel deliveryAddressModel, string username);
        public bool UpdateAddress(DeliveryAddressModel deliveryAddressModel);

        public bool DeleteAddress(int id);
       /* public void CheckAllFalse(string username);*/
    }
}
