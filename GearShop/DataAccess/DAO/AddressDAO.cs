using BusinessObject.Core;
using BusinessObject.Model.Entity;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        Customer = address.Customer
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

        public static bool UpdateAddress(DeliveryAddressModel addressModel)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    var existingAddress = context.DeliveryAddresses
                                                 .FirstOrDefault(x => x.Id == addressModel.Id && x.Username == addressModel.Username);

                    if (existingAddress != null)
                    {
                        // Map properties from DeliveryAddressModel to DeliveryAddress entity
                        existingAddress.Fullname = addressModel.Fullname;
                        existingAddress.Phone = addressModel.Phone;
                        existingAddress.Address = addressModel.Address;
                        existingAddress.Specific = addressModel.Specific;
                        existingAddress.IsDefault = addressModel.IsDefault;

                        // Mark the address as modified
                        context.Entry(existingAddress).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Address not found");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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

        public static DeliveryAddressModel? FindExistingAddressItem(string username, string phoneNumber, string fullname, string address, bool isDefault)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    // Query the database to find an address that matches all the parameters
                    var existingAddress = context.DeliveryAddresses
                        .FirstOrDefault(a => a.Username == username
                                             && a.Phone == phoneNumber
                                             && a.Fullname == fullname
                                             && a.Address == address
                                             && a.IsDefault == isDefault);

                    if (existingAddress != null)
                    {
                        // Map the entity to DeliveryAddressModel DTO
                        return new DeliveryAddressModel
                        {
                            Id = existingAddress.Id,
                            Username = existingAddress.Username,
                            Fullname = existingAddress.Fullname,
                            Phone = existingAddress.Phone,
                            Address = existingAddress.Address,
                            Specific = existingAddress.Specific,
                            IsDefault = existingAddress.IsDefault,
                            Customer = existingAddress.Customer,
                        };
                    }

                    // Return null if no matching address is found
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
