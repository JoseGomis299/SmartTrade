using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartTrade.ViewModels
{
    public class SellerRegisterModel
    {
        public async Task RegisterSeller(string email, string password, string name, string lastnames, string cif, string company, string iban)
        {
            await SmartTradeService.Instance.RegisterSellerAsync(email,password,name,lastnames,cif, company,iban);
        }
        public void ValidarDniCif(string dniCif)
        {

            string patternDni = @"^\d{8}[A-Za-z]$";
            string patternCif = @"^[A-Z]\d{8}$";
            if (!Regex.IsMatch(dniCif, patternDni) & !Regex.IsMatch(dniCif, patternCif))
            {
                throw new ArgumentException("Incorrect CIF/DNI. Please enter a valid CIF/DNI");
            }
        }
    }
}
