using BusinessObject.Model.Entity;
using DataAccess.DAO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class AddressRepository : IAddressRepository
    {

        public List<DeliveryAddressModel> GetAllAddress(string username)
            => AddressDAO.GetAllAddress(username);

        public bool AddNewAddress(DeliveryAddressModel deliveryAddressModel, string username)
        {
            deliveryAddressModel.Username = username;
            var existingAddressItem = AddressDAO.FindExistingAddressItem(deliveryAddressModel.Username, deliveryAddressModel.Phone, deliveryAddressModel.Fullname, deliveryAddressModel.Address, deliveryAddressModel.IsDefault);
            if (existingAddressItem != null)
            {
                return false;
            }
            else
            {
                return AddressDAO.AddNewAddressPartialView(deliveryAddressModel, username);
            }
        }

        public bool UpdateAddress(DeliveryAddressModel deliveryAddressModel)
        {
            var existingAddressItem = AddressDAO.FindExistingAddressItem(deliveryAddressModel.Username, deliveryAddressModel.Phone, deliveryAddressModel.Fullname, deliveryAddressModel.Address, deliveryAddressModel.IsDefault);
            if (existingAddressItem != null)
            {
                return false; // Address already exist 
            }
            else
            {
                AddressDAO.UpdateAddress(deliveryAddressModel);
                return true;
            }
        }

        public bool DeleteAddress(int id) => AddressDAO.DeleteAddress(id);

    }
}
