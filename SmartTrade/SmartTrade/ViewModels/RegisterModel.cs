using Microsoft.CodeAnalysis.CSharp.Syntax;
using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels
{
    public class RegisterModel : ViewModelBase
    {
        public UserDTO Logged => SmartTradeService.Instance.Logged;
        public DateTime ConvertDate(string dateString)
        {
            DateTime date;
            try
            {
                date = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return date;
            }
            catch (FormatException)
            {
                throw new Exception("Incorrect format");
            }
        }

        public void ValidarDni(string dni)
        {
            string pattern = @"^\d{8}[A-Za-z]$";
            if (!Regex.IsMatch(dni, pattern))
            {
                throw new ArgumentException("Incorrect DNI. Must be 8 digits followed by a letter");
            }
        }

        public void ValidarEmail(string email)
            {
                string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
                if (!Regex.IsMatch(email, pattern))
                {
                    throw new ArgumentException("Wrong email. Please enter a valid email");
                }
        }
        public void ValidarTelefono(string telefono)
        {
            string pattern = @"^\d+$";
            if (!Regex.IsMatch(telefono, pattern))
            {
                throw new ArgumentException("Wrong phone number. Only digits are allowed");
            }
        }

  
    
    public async Task RegisterConsumer(string email, string password, string name, string lastnames, string dni, DateTime dateBirth, Address billingAddress, Address consumerAddress)
        {
           await SmartTradeService.Instance.RegisterConsumerAsync(email, password, name, lastnames, dni, dateBirth, billingAddress, consumerAddress);
        }
    }        
}
