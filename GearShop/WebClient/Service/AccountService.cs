using BusinessObject.DTOS;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using WebClient.Models;
using WebClient.APIEndPoint;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace WebClient.Service
{
    public class AccountService
    {
        private readonly HttpClient client = null;
        private readonly EmailSettings _emailSettings;
        public AccountService(IOptions<EmailSettings> emailSettings)
        {
            client = new HttpClient();
            _emailSettings = emailSettings.Value;
        }

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


        public string VerifyEmail(string email)
        {
            try
            {
                string reciever = email;
                Random random = new Random();

                string otp = random.Next(100000, 999999).ToString();
                Console.WriteLine($"OTP created: {otp}");
                DateTime date = DateTime.Now;

                string resxFilePath = "WebClient.Resource.Template";

                ResourceManager resourceManager = new ResourceManager(resxFilePath, Assembly.GetExecutingAssembly());

                string htmlContent = resourceManager.GetString("Email");
                htmlContent = htmlContent.Replace("@param01", date.ToString());
                htmlContent = htmlContent.Replace("@param03", otp);
                htmlContent = htmlContent.Replace("@param04", _emailSettings.FromEmail);

                MailMessage message = new MailMessage();
                message.From = new MailAddress(_emailSettings.FromEmail);
                message.Subject = "The OTP to reset password";
                message.To.Add(new MailAddress(reciever));
                message.Body = htmlContent;
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(_emailSettings.FromEmail, _emailSettings.EmailPassword),
                    EnableSsl = true,
                };

                // Send the email
                try
                {
                    smtpClient.Send(message);
                    return otp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                }

                return otp;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return null;
            }
        }
        public bool ForgetPassword(string password, string emailSend)
        {
            string hashPass = CalculateMD5Hash(password);
            Console.WriteLine("pass forget" + hashPass);
            string url = string.Format(ApiEndpoints_Customer.FORGET_PASSWORD, emailSend, hashPass);
            client.GetAsync(url);
            return true;
        }

        public async Task<bool> RegisterAsync(RegistModel registModel)
        {
            try
            {
                // Hash the password using MD5
                string hashPass = CalculateMD5Hash(registModel.Password);
                registModel.Password = hashPass;

                Console.WriteLine("Hashed password: " + hashPass);

                // Construct the API endpoint URL for registration
                string url = ApiEndpoints_Customer.REGISTER;

                // Convert the registration model to JSON
                var jsonContent = JsonConvert.SerializeObject(registModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Make an asynchronous request to the registration endpoint using POST
                var response = await client.PostAsync(url, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to register. Status code: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during registration: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return false;
            }
        }

    }
}
