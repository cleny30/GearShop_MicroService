using BusinessObject.Model.Entity;
using DataAccess.DAO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DeliveryAddressModel? FindExistingAddressItem(string username, string phoneNumber, string fullname, string address, bool isdefault)
            => AddressDAO.FindExistingAddressItem(username, phoneNumber, fullname, address, isdefault);
        public void CheckAllFalse(string username)=> AddressDAO.GetAddressByUsername(username);*/

    }
}
