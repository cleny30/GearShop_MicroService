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
        public static async Task<bool> CheckUsername(String username)
        {

            using (var context = new CustomerContext())
            {
                var customer = await context.Customers.FirstOrDefaultAsync(c => c.Username == username);
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
        public static async Task<bool> Register(RegisterModel userRegist)
        {
            try
            {
                using (var dbContext = new CustomerContext())
                {
                    Customer customer = new Customer();
                    customer.CopyProperties(userRegist);

                    dbContext.Customers.Add(customer);

                    await dbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log exception nếu cần thiết
                Console.WriteLine(ex.Message);
                return false;
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
    }
}
