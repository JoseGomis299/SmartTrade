using SmartTradeLib.BusinessLogic;
using SmartTradeLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartTrade.ViewModels
{
    public class LoginModel : ViewModelBase
    {
        public User? Logged => MainViewModel.SmartTradeService.Logged;

        public void Login(string email, string password)
        {
            MainViewModel.SmartTradeService.LogIn(email, password);
        }
    }
}
