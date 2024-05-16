using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;
using SmartTrade.Entities;
using System.Text.RegularExpressions;
using System;
using SmartTrade.Services;


namespace SmartTrade.ViewModels
{
    public class LoginModel : ViewModelBase
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserDTO Logged => Service.Logged;

        public void ValidarEmail()
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            if (!Regex.IsMatch(Email, pattern))
            {
                throw new ArgumentException("Invalid email. Please enter a valid email");
            }
        }

        public async Task Login(string email, string password)
        {
            await Service.LogInAsync(email, password);
        }
    }
}
