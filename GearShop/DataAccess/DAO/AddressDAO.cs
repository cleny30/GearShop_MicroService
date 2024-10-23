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

        public static bool AddNewAddress(DeliveryAddressModel addressModel, string username)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    // Kiểm tra nếu đã có địa chỉ nào với Id này trong DbContext
                    var existingAddress = context.DeliveryAddresses
                                                 .FirstOrDefault(a => a.Id == addressModel.Id);

                    if (existingAddress != null)
                    {
                        throw new Exception("Address with the same Id already exists.");
                    }

                    // Đếm số lượng địa chỉ của người dùng
                    int addressCount = context.DeliveryAddresses
                                              .Where(a => a.Username == username)
                                              .Count();

                    // Kiểm tra nếu số lượng địa chỉ đã đạt tối đa
                    if (addressCount >= 5)
                    {
                        throw new Exception("Người dùng chỉ có thể thêm tối đa 5 địa chỉ vận chuyển.");
                    }

                    // Thêm địa chỉ mới
                    var addressEntity = new DeliveryAddress();
                    addressEntity.CopyProperties(addressModel);

                    context.DeliveryAddresses.Add(addressEntity);  // Chỉ thêm nếu chưa tồn tại
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
                    // Kiểm tra xem địa chỉ có tồn tại không
                    var existingAddress = context.DeliveryAddresses
                                                 .FirstOrDefault(x => x.Id == addressModel.Id && x.Username == addressModel.Username);

                    if (existingAddress == null)
                    {
                        // Thêm thông tin chi tiết vào lỗi
                        throw new Exception($"Address with Id {addressModel.Id} and Username {addressModel.Username} not found.");
                    }

                    // Nếu địa chỉ mới được đặt làm mặc định, tất cả các địa chỉ khác sẽ không còn mặc định
                    if (addressModel.IsDefault)
                    {
                        var existingDefaultAddresses = context.DeliveryAddresses
                                                              .Where(a => a.Username == addressModel.Username && a.IsDefault)
                                                              .ToList();
                        foreach (var existingAddr in existingDefaultAddresses)
                        {
                            existingAddr.IsDefault = false;
                            context.Entry(existingAddr).State = EntityState.Modified;
                        }
                    }

                    // Map properties từ DeliveryAddressModel sang entity
                    existingAddress.Fullname = addressModel.Fullname;
                    existingAddress.Phone = addressModel.Phone;
                    existingAddress.Address = addressModel.Address;
                    existingAddress.Specific = addressModel.Specific;
                    existingAddress.IsDefault = addressModel.IsDefault;

                    // Đánh dấu entity là đã sửa đổi
                    context.Entry(existingAddress).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                // Hiển thị thêm thông tin chi tiết nếu cần thiết
                throw new Exception($"Error updating address: {e.Message}");
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
        public static List<DeliveryAddressModel> GetAddressByUsername(string username)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    // Giả sử bạn có một entity trong DB là DeliveryAddress
                    var addresses = context.DeliveryAddresses
                                           .Where(a => a.Username == username)
                                           .Select(a => new DeliveryAddressModel
                                           {
                                               Id = a.Id,
                                               Username = a.Username,
                                               Fullname = a.Fullname,
                                               Phone = a.Phone,
                                               Address = a.Address,
                                               Specific = a.Specific,
                                               IsDefault = a.IsDefault
                                           }).ToList();

                    return addresses;
                }
            }
            catch (Exception e)
            {
                // Ghi lại lỗi hoặc thực hiện các hành động cần thiết
                Console.WriteLine($"Error occurred: {e.Message}");
                return new List<DeliveryAddressModel>(); // Trả về danh sách trống khi xảy ra lỗi
            }

        }
    }
}
