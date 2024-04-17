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
        public string? Name { get; set; }
        public string? LastNames { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? DNI { get; set; }
        public string? DateBirth { get; set; }
        public string? Number { get; set; }
        public string? Province { get; set; }
        public string? Municipality { get; set; }
        public string? PostalCode { get; set; }
        public string? Street { get; set; }
        public string? Door { get; set; }
        public string? PhoneNumber { get; set; }

        public string PaypalEmail
        {
            get => _Paypalemail;
            set
            {
                _Paypalemail = value;
            }
        }

        public static string _Paypalemail;
        public static string _Paypalpassword;

        public string PaypalPassword
        {
            get => _Paypalpassword;
            set
            {
                _Paypalpassword = value;
            }
        }

        public void Validar()
        {
            ValidarDni();
            ValidarEmail();
        }


        public DateTime ConvertDate()
        {
            DateTime date;
            try
            {
                date = DateTime.ParseExact(DateBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return date;
            }
            catch (FormatException)
            {
                throw new Exception("Incorrect format");
            }
        }

        public void ValidarDni()
        {
            string pattern = @"^\d{8}[A-Za-z]$";
            if (!Regex.IsMatch(DNI, pattern))
            {
                throw new ArgumentException("Incorrect DNI. Must be 8 digits followed by a letter");
            }
        }

        public void ValidarEmail()
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            if (!Regex.IsMatch(Email, pattern))
            {
                throw new ArgumentException("Wrong email. Please enter a valid email");
            }
        }

        public async Task RegisterConsumer()
        {
            Validar();
            Address consumerAddress = new Address(Province, Street, Municipality, PostalCode, Number, Door);

            await SmartTradeService.Instance.RegisterConsumerAsync(Email, Password, Name, LastNames, DNI, ConvertDate(), consumerAddress, consumerAddress);
            UserDTO Logged = SmartTradeService.Instance.Logged;
            if (_Paypalemail != null && _Paypalpassword != null)
            {
                PayPalInfo paypalData = new PayPalInfo(_Paypalemail, _Paypalpassword);
            }
        }
    }        
}
