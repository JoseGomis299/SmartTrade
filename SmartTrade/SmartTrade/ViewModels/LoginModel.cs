using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;
using SmartTrade.Entities;
using System.Text.RegularExpressions;
using System;


namespace SmartTrade.ViewModels
{
    public class LoginModel : ViewModelBase
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserDTO Logged => SmartTradeService.Instance.Logged;

        public void ValidarEmail()
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            if (!Regex.IsMatch(Email, pattern))
            {
                throw new ArgumentException("Wrong email. Please enter a valid email");
            }
        }

        public async Task Login(string email, string password)
        {
            try
            {
                await SmartTradeService.Instance.LogInAsync(email, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
