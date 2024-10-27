using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class CustomerDAO
    {
        public static CustomerModel GetCustomerByUserName(string username)
        {
            var customer = new CustomerModel();
            try
            {
                using (var context = new CustomerContext())
                {
                    Customer customer1 = new Customer();
                    customer1 = context.Customers.ToList().FirstOrDefault(x => x.Username == username);
                    customer.CopyProperties(customer1);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return customer;
        }


        public static void UpdateCustomer(CustomerModel customerDTO)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    // Tìm khách hàng trong cơ sở dữ liệu bằng ID từ customerDTO
                    var customer = context.Customers.FirstOrDefault(x => x.Username == customerDTO.Username);

                    if (customer != null)
                    {
                        // Cập nhật các thuộc tính từ customerDTO vào customer
                        customer.CopyProperties(customerDTO);

                        // Đánh dấu khách hàng đã được cập nhật
                        context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        // Lưu thay đổi vào cơ sở dữ liệu
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Customer not found");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static async Task<LoginAccountModel> ClientLogin(string username, string pass)
        {
            using (var context = new CustomerContext())
            {
                var customer = await context.Customers.FirstOrDefaultAsync(c => c.Username == username && c.Password == pass);
                if (customer != null)
                {

                    var loginAccount = new LoginAccountModel();
                    loginAccount.CopyProperties(customer);

                    return loginAccount;
                }
            }
            return null;

        }

        public static async Task<Customer> ClientChangePassword(ChangePasswordModel model)
        {

            using (var context = new CustomerContext())
            {
                var customer = await context.Customers.FirstOrDefaultAsync(c => c.Username == model.Username);
                if (customer != null)
                {
                    if (customer.Password != model.OldPassword) throw new Exception("Old Password not correct");


                    customer.Password = model.NewPassword;

                    context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    await context.SaveChangesAsync();

                    return customer;
                }
                else
                {
                    throw new Exception("Customer not found");
                }
            }

        }

        public static async Task<bool> CheckMail(String mail)
        {

            using (var context = new CustomerContext())
            {
                var customer = await context.Customers.FirstOrDefaultAsync(c => c.Email == mail);
                if (customer != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public static void ForgetPassword(string mail, string pass)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    var customer = context.Customers.FirstOrDefault(x => x.Email == mail);

                    if (customer != null)
                    {                 
                        customer.Password = pass;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Customer not found");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /*public static void Register(string username, string fullname, string phone, string email)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    // Tìm kiếm customer dựa trên username
                    var customer = context.Customers.FirstOrDefault(x => x.Username == username);

                    if (customer != null)
                    {
                        throw new Exception("Customer is already");

                    }
                    else
                    {
                        // Tạo mới customer nếu chưa tồn tại
                        customer = new Customer
                        {
                            Username = username,
                            Fullname = fullname,
                            Phone = phone,
                            Email = email
                        };

                        context.Customers.Add(customer); // Lưu customer mới vào database
                        context.SaveChanges(); // Lưu thay đổi vào database
                    }

                   
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }*/



        public static void Register(RegistModel userRegist)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    // Tìm kiếm customer dựa trên username từ userRegist
                   var customer21 = context.Customers.FirstOrDefault(x => x.Username == userRegist.Username);

                    if (customer21 == null)
                    {
                        // Tạo mới đối tượng customer nếu chưa tồn tại
                      var  customer = new Customer
                        {
                            Username = userRegist.Username,
                            Password = userRegist.Password,
                            Fullname = userRegist.Fullname,
                            Phone = userRegist.Phone,
                            Email = userRegist.Email
                        };

                        context.Customers.Add(customer); // Lưu customer mới vào database
                        context.SaveChanges(); // Lưu thay đổi vào database
                    }
                    else
                    {
                        throw new Exception("Customer already exists");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /*public static async Task<bool> RegisterCustomer(CustomerModel customerDTO)
        {
            try
            {
                using (var context = new CustomerContext())
                {
                    // Kiểm tra xem username hoặc email đã tồn tại trong cơ sở dữ liệu chưa
                    var existingCustomer = await context.Customers.FirstOrDefaultAsync(c =>
                        c.Username == customerDTO.Username || c.Email == customerDTO.Email);

                    if (existingCustomer != null)
                    {
                        throw new Exception("Username or email already exists");
                    }

                    // Tạo một thực thể Customer mới và sao chép các thuộc tính từ customerDTO
                    var customer = new Customer();
                    customer.CopyProperties(customerDTO);

                    // Thêm khách hàng mới vào cơ sở dữ liệu
                    await context.Customers.AddAsync(customer);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await context.SaveChangesAsync();

                    return true; // Trả về true nếu đăng ký thành công
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }*/


        /* public static async Task<bool> RegisterCustomer(CustomerModel customerDTO)
         {
             try
             {
                 using (var context = new CustomerContext())
                 {
                     // Check if the username or email already exists in the database
                     var existingCustomer = await context.Customers.FirstOrDefaultAsync(c =>
                         c.Username == customerDTO.Username || c.Email == customerDTO.Email);

                     if (existingCustomer != null)
                     {
                         throw new Exception("Username or email already exists");
                     }

                     // Create a new Customer entity and copy properties from the DTO
                     var customer = new Customer();
                     customer.CopyProperties(customerDTO);

                     // Add the new customer to the database
                     await context.Customers.AddAsync(customer);

                     // Save changes to the database
                     await context.SaveChangesAsync();

                     return true; // Return true if the registration is successful
                 }
             }
             catch (Exception e)
             {
                 throw new Exception(e.Message);
             }
         }*/




    }
}
