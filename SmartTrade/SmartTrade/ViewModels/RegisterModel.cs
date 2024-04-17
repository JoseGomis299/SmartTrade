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

        public string _Paypalemail;
        public string _Paypalpassword;

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
        public string CreditCardNumber { get;set; }
        public string CreditCardName { get; set; }
        public string CreditCardExpiryDate { get; set; }
        public string CreditCardCVV { get; set; } 
        public string BizumNumber { get; set; } 


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
        public DateTime ConvertExpiryDate()
        {
            DateTime resultado;
            string formato = "MM/yy";

            if (DateTime.TryParseExact(CreditCardExpiryDate, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out resultado))
            {
                return resultado;
            }
            else
            {
                throw new ArgumentException("Incorrect format");
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
            string pattern = @"^\d+$";
            if (!Regex.IsMatch(PhoneNumber, pattern))
            {
                throw new ArgumentException("Wrong phone number. Only digits are allowed");
            }

            if (_Paypalemail != null && _Paypalpassword != null)
            {
                PayPalInfo paypalData = new PayPalInfo(_Paypalemail, _Paypalpassword);
                await SmartTradeService.Instance.AddPaypalAsync(paypalData, PaypalEmail);
            }
            if(CreditCardCVV != null && CreditCardName !=null && CreditCardNumber !=null && CreditCardExpiryDate != null)
            {
                CreditCardInfo creditCard = new CreditCardInfo(CreditCardNumber,ConvertExpiryDate(),CreditCardCVV,CreditCardName);
                await SmartTradeService.Instance.AddCreditCardAsync(creditCard);
            }
            if (BizumNumber != null)
            {
                BizumInfo bizum = new BizumInfo(BizumNumber);
                await SmartTradeService.Instance.AddBizumAsync(bizum);
            }
        }

        public void ValidarNumeroTarjeta()
        {
            string pattern = @"^\d+$";
            if (!Regex.IsMatch(CreditCardNumber, pattern))
            {
                throw new ArgumentException("Wrong number card. Only digits are allowed");
            }
        }

        public void ValidarCVV()
        {
            string pattern = @"^\d+$";
            if (!Regex.IsMatch(CreditCardCVV, pattern))
            {
                throw new ArgumentException("Wrong cvv. Only digits are allowed");
            }
        }

    }        
}
