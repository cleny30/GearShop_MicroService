using BusinessObject.Core;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ManagerDAO
    {
        public static List<Manager> GetManagers()
        {
            var list = new List<Manager>();
            try
            {
                using (var context = new ManagerContext())
                {
                    list = context.Managers.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }

        public static async Task<ManagerModel> GetManagerByUsername(string username)
        {
            try
            {
                using (var dbContext = new ManagerContext())
                {
                    Manager _manager = await dbContext.Managers.FirstOrDefaultAsync(x => x.Username == username);
                    ManagerModel model = new ManagerModel();
                    model.CopyProperties(_manager);
                    return model;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as necessary
                return null;
            }
        }

        public static async Task<Boolean> CheckUsernameExistedAsync(string username)
        {
            try
            {
                using (var dbContext = new ManagerContext())
                {
                    Manager _manager = await dbContext.Managers.FirstOrDefaultAsync(x => x.Username == username);
                    return _manager != null;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as necessary
                return false;
            }
        }

        public static async Task<Boolean> CheckManagerExistedAsync(string username, string password)
        {
            try
            {
                using (var dbContext = new ManagerContext())
                {
                    string hashedPassword = GetMD5Hash(password);
                    Manager _manager = await dbContext.Managers.FirstOrDefaultAsync(m => m.Username.Equals(username) && m.Password.Equals(hashedPassword));
                    return _manager != null;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as necessary
                return false;
            }
        }

        public static string GetMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input); // Convert the input string to bytes
                byte[] hashBytes = md5.ComputeHash(inputBytes); // Compute the MD5 hash

                // Convert the byte array to a hexadecimal string representation of the hash
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // "x2" means lowercase hexadecimal
                }
                return sb.ToString();
            }
        }
    }
}
