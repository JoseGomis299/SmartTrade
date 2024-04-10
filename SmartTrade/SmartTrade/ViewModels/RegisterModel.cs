using Microsoft.CodeAnalysis.CSharp.Syntax;
using SmartTradeLib.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.ViewModels
{
    public class RegisterModel : ViewModelBase
    {
        public DateTime convertDate(string dateString)
        {
            DateTime date;
            try
            {
                date = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return date;
            }
            catch (FormatException)
            {
                throw new Exception("formato incorrecto");
            }
        }

        internal void RegisterConsumer(string email, string password, string name, string lastnames, string dni, DateTime dateBirth, Address billingAddress, Address consumerAddress)
        {
            MainViewModel.SmartTradeService.RegisterConsumer(email, password, name, lastnames, dni, dateBirth, billingAddress, consumerAddress);
        }
    }        
}
