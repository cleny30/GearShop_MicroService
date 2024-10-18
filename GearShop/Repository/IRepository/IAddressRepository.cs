using BusinessObject.DTOS;
using BusinessObject.Model.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IAddressRepository
    {
      /*  public bool AddNewAddress(DeliveryAddressModel deliAddressModel);*/
        public List<DeliveryAddressModel> GetAllAddress(string username);
      /*  public bool UpdateAddress(DeliveryAddressModel deliveryAddressModel);
        public bool DeleteAddress(string username, int id);
        public DeliveryAddressModel? FindExistingAddressItem(string username, string phoneNumber, string fullname, string address, bool isdefault);
        public void CheckAllFalse(string username);*/
    }
}
