using BusinessObject.Core;
using BusinessObject.Model.Entity;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class AddressDAO
    {
        public static List<DeliveryAddressModel> GetAllAddress(string username)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    
                    var addresses = context.DeliveryAddresses
                                           .Where(x => x.Username == username)
                                           .OrderByDescending(x => x.IsDefault)
                                           .ToList();

                    
                    var addressModels = addresses.Select(address => new DeliveryAddressModel
                    {
                        Id = address.Id,
                        Username = address.Username,
                        Fullname = address.Fullname,
                        Phone = address.Phone,
                        Address = address.Address,
                        Specific = address.Specific,
                        IsDefault = address.IsDefault,
                    }).ToList();

                    return addressModels;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static bool AddNewAddress(DeliveryAddressModel addressModel)
        {
            try
            {
                using (var context = new CustomerContext())
                {

                    var addressEntity = new DeliveryAddress();
                    addressEntity.CopyProperties(addressModel);

                    context.DeliveryAddresses.Add(addressEntity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void SetDefaultAddress(string username, int id)
        {
            using (var context = new CustomerContext())
            {
                // Retrieve all addresses for the given username
                var addresses = context.DeliveryAddresses.Where(c => c.Username == username).ToList();

                // Check if this is the first address being added for the user
                if (addresses.Count == 1)
                {
                    // If there are no existing addresses, set the new address as default
                    var newAddress = context.DeliveryAddresses.FirstOrDefault(c => c.Id == id);
                    if (newAddress != null)
                    {
                        newAddress.IsDefault = true;
                        context.SaveChanges();
                    }
                    return;
                }
                else
                {
                    // Set all existing addresses for the given username to IsDefault = false
                    foreach (var address in addresses)
                    {
                        address.IsDefault = false;
                    }

                    //// Find the address with the specified id and set IsDefault = true
                    //var defaultAddress = addresses.FirstOrDefault(c => c.Id == id);
                    //if (defaultAddress != null)
                    //{
                    //    defaultAddress.IsDefault = true;
                    //}

                    // Save changes to the database
                    context.SaveChanges();
                }
            }
        }

        public static bool AddNewAddressPartialView(DeliveryAddressModel deliveryAddressModel, string username)
        {
            using (var dbContext = new CustomerContext())
            {
                // Query the DeliveryAddress table to get the newest address ID
                int newestAddressId = dbContext.DeliveryAddresses
                                             .OrderByDescending(a => a.Id)
                                             .Select(a => a.Id)
                                             .FirstOrDefault();

                deliveryAddressModel.Id = newestAddressId + 1;
                AddNewAddress(deliveryAddressModel);

                //if (deliveryAddressModel.IsDefault)
                //{
                //    SetDefaultAddress(username, deliveryAddressModel.Id);
                //}
                return true;
            }
        }

        public static bool UpdateAddress(DeliveryAddressModel deliveryAddressModel)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    var existingAddress = context.DeliveryAddresses.FirstOrDefault(p => p.Id == deliveryAddressModel.Id);
                    if (existingAddress != null)
                    {
                        // Update product information
                        existingAddress.Fullname = deliveryAddressModel.Fullname;
                        existingAddress.Phone = deliveryAddressModel.Phone;
                        existingAddress.Address = deliveryAddressModel.Address;
                        existingAddress.Specific = deliveryAddressModel.Specific;

                        context.SaveChanges();

                        //if (deliveryAddressModel.IsDefault)
                        //{
                        //    SetDefaultAddress(deliveryAddressModel.Username, existingAddress.Id);
                        //}
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteAddress(int id)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    var address = context.DeliveryAddresses
                                         .FirstOrDefault(x => x.Id == id);

                    if (address != null)
                    {
                        // Remove the address from the context
                        context.DeliveryAddresses.Remove(address);
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static DeliveryAddressModel? FindExistingAddressItem(string username, string phoneNumber, string fullname, string address, bool isdefault)
        {
            using (var context = new CustomerContext())
            {
                var deliveryAddress = context.DeliveryAddresses.FirstOrDefault(c => c.Username == username && c.Phone == phoneNumber && c.Fullname == fullname && c.Address == address && c.IsDefault == isdefault);
                if (deliveryAddress != null)
                {
                    DeliveryAddressModel deliveryAddressModel = new DeliveryAddressModel();
                    deliveryAddressModel.CopyProperties(deliveryAddress);
                    return deliveryAddressModel;
                }
                return null;
            }
        }
    }
}
