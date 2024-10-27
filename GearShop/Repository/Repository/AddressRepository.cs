using BusinessObject.Model.Entity;
using DataAccess.DAO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class AddressRepository : IAddressRepository
    {

       /* public bool AddNewAddress(DeliveryAddressModel deliAddressModel)
            => AddressDAO.AddNewAddress(deliAddressModel);*/
        public List<DeliveryAddressModel> GetAllAddress(string username)
            => AddressDAO.GetAllAddress(username);

        /* public bool UpdateAddress(DeliveryAddressModel deliveryAddressModel)
             => AddressDAO.UpdateAddress(deliveryAddressModel);
         public bool DeleteAddress(string username, int id)
             => AddressDAO.DeleteAddress(username, id);

         public void CheckAllFalse(string username)=> AddressDAO.GetAddressByUsername(username);*/

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
                AddressDAO.CheckAllFalse(deliveryAddressModel.Username);
                return true;
            }
        }
    }
}
