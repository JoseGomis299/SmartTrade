using Microsoft.CodeAnalysis.CSharp.Syntax;
using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartTrade.ViewModels
{
    public class RegisterModel : ViewModelBase
    {
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
                throw new Exception("formato incorrecto");
            }
        }

        public static void ValidarDni(string dni)
        {
            string pattern = @"^\d{8}[A-Za-z]$";
            if (!Regex.IsMatch(dni, pattern))
            {
                throw new ArgumentException("DNI incorrecto. Debe tener 8 dígitos seguidos de una letra.");
            }
        }

        public static void ValidarEmail(string email)
            {
                string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
                if (!Regex.IsMatch(email, pattern))
                {
                    throw new ArgumentException("Email incorrecto. Por favor, introduce un email válido.");
                }
        }
        public static void ValidarTelefono(string telefono)
        {
            string pattern = @"^\d+$";
            if (!Regex.IsMatch(telefono, pattern))
            {
                throw new ArgumentException("Número de teléfono incorrecto. Solo se permiten dígitos.");
            }
        }

        public async Task RegisterConsumer(string email, string password, string name, string lastnames, string dni, DateTime dateBirth, Address billingAddress, Address consumerAddress)
            {
           await SmartTradeService.Instance.RegisterConsumerAsync(email, password, name, lastnames, dni, dateBirth, billingAddress, consumerAddress);
            }
    }        
}
