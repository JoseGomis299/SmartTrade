﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public string CreditCardNumber { get;set; }
        public string CreditCardName { get; set; }
        public string CreditCardExpiryDate { get; set; }
        public string CreditCardCVV { get; set; }



        public void ConvertDate(string dateString)
        {
            DateTime date;
            try
            {
                date = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                throw new Exception("Incorrect format");
            }
        }
        public DateTime ConvertExpiryDate(string fecha)
        {
            DateTime resultado;
            string formato = "MM/yy";

            if (DateTime.TryParseExact(fecha, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out resultado))
            {
                return resultado;
            }
            else
            {
                throw new ArgumentException("Incorrect format");
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
        public void ValidarNumeroTarjeta(string numeroTarjeta)
        {
            string pattern = @"^\d+$";
            if (!Regex.IsMatch(numeroTarjeta, pattern))
            {
                throw new ArgumentException("Wrong number card. Only digits are allowed");
            }
        }
        public void ValidarCVV(string cvv)
        {
            string pattern = @"^\d+$";
            if (!Regex.IsMatch(cvv, pattern))
            {
                throw new ArgumentException("Wrong cvv. Only digits are allowed");
            }
        }


        public async Task RegisterConsumer(string email, string password, string name, string lastnames, string dni, DateTime dateBirth, Address billingAddress, Address consumerAddress)
        {
            await SmartTradeService.Instance.RegisterConsumerAsync(email, password, name, lastnames, dni, dateBirth, billingAddress, consumerAddress);
            UserDTO Logged = SmartTradeService.Instance.Logged;
            if (_Paypalemail != null && _Paypalpassword != null)
            {
            PayPalInfo paypalData = new PayPalInfo(_Paypalemail, _Paypalpassword);
            await SmartTradeService.Instance.AddPaypalAsync(paypalData, email);  
            }
            if(CreditCardCVV != null && CreditCardName !=null && CreditCardNumber !=null && CreditCardExpiryDate != null)
            {
                CreditCardInfo creditCard = new CreditCardInfo(CreditCardNumber,CreditCardExpiryDate,CreditCardCVV,CreditCardName);
                await SmartTradeService.Instance.AddCreditCardAsync(creditCard);

            }
        }
    }        
}
