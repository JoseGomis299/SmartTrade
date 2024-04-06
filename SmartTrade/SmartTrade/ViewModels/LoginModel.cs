using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.ViewModels
{
    public class LoginModel : ViewModelBase
    {
        string name;
        string password;
        public LoginModel(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        internal void Login(string email, string password)
        {
            MainViewModel.SmartTradeService.LogIn(email, password);
        }
    }
}
