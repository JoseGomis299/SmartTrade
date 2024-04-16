using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;
using SmartTrade.Entities;


namespace SmartTrade.ViewModels
{
    public class LoginModel : ViewModelBase
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserDTO Logged => SmartTradeService.Instance.Logged;
        public async Task Login(string email, string password)
        {
           await SmartTradeService.Instance.LogInAsync(email, password);
        }
    }
}
