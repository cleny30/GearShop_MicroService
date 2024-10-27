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
        public static void CheckAllFalse(string username)
        {
            using (var context = new CustomerContext())
            {
                bool hasDefaultAddress = context.DeliveryAddresses.Any(c => c.Username == username && c.IsDefault == true);
                if (!hasDefaultAddress)
                {
                    var firstAddress = context.DeliveryAddresses
                                              .Where(c => c.Username == username)
                                              .OrderBy(c => c.Id)
                                              .FirstOrDefault();

                    if (firstAddress != null)
                    {
                        firstAddress.IsDefault = true;
                        context.SaveChanges();
                    }
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
                CheckAllFalse(username);
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
                        // Check if isDefault is being set to true
                        if (deliveryAddressModel.IsDefault)
                        {
                            // Find all addresses with the same username and set their isDefault to false
                            var otherAddresses = context.DeliveryAddresses.Where(p => p.Username == deliveryAddressModel.Username && p.Id != existingAddress.Id).ToList();
                            foreach (var address in otherAddresses)
                            {
                                address.IsDefault = false;
                            }
                        }

                        existingAddress.IsDefault = deliveryAddressModel.IsDefault;
                        context.SaveChanges();
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

        public static bool DeleteAddress(string username, int id)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    var address = context.DeliveryAddresses
                                         .FirstOrDefault(x => x.Username == username && x.Id == id);

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
