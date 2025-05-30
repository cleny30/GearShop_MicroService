﻿using BusinessObject.DTOS;
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
                message.Subject = "OTP Check";
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
    }
}
