using BusinessObject.DTOS;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using WebClient.APIEndPoint;

namespace WebClient.Service
{
    public class AccountService
    {
   
        public ValidateResult Validate(LoginAccountModel userLogin)
        {
            ValidateResult validateResult = new ValidateResult();
            if (userLogin == null)
            {
                validateResult.AddError("", "Login error");
                return validateResult;
            }
            if (string.IsNullOrEmpty(userLogin.Username))
            {
                validateResult.AddError(nameof(LoginAccountModel.Username), "Username can't be empty");
            }
            else if (userLogin.Username.Length > 50)
            {
                validateResult.AddError(nameof(LoginAccountModel.Username), "Maxlength of username is 50 characters");
            }

            if (string.IsNullOrEmpty(userLogin.Password))
            {
                validateResult.AddError(nameof(LoginAccountModel.Password), "Password can't be empty");
            }
            else if (userLogin.Password.Length > 32)
            {
                validateResult.AddError(nameof(LoginAccountModel.Password), "Maxlength of password is 32 characters");
            }
            return validateResult;
        }

        public string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
