using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartTrade.ViewModels
{
    public class SellerRegisterModel : ViewModelBase
    {
        public string? Name { get; set; }
        public string? LastNames { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? CIF { get; set; }
        public string? Company { get; set; }
        public string? IBAN { get; set; }

        public async Task RegisterSeller()
        {
            ValidarDniCif();
            await SmartTradeService.Instance.RegisterSellerAsync(Email, Password, Name, LastNames,CIF, Company, IBAN);
        }
        public void ValidarDniCif()
        {
            string patternDni = @"^\d{8}[A-Za-z]$";
            string patternCif = @"^[A-Z]\d{8}$";
            if (!Regex.IsMatch(CIF, patternDni) & !Regex.IsMatch(CIF, patternCif))
            {
                throw new ArgumentException("Incorrect CIF/DNI. Please enter a valid CIF/DNI");
            }
        }
    }
}
