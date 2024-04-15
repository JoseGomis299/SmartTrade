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
        private string _email;
        private string _password;
        private string _name;
        private string _lastNames;
        private string _cif;
        private string _company;
        private string _iban;
        public string email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }
        public string name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public string password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }
        public string lastNames
        {
            get => _lastNames;
            set => this.RaiseAndSetIfChanged(ref _lastNames, value);
        }
        public string cif
        {
            get => _cif;
            set => this.RaiseAndSetIfChanged(ref _cif, value);
        }
        public string company
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _company, value);
        }
        public string iban
        {
            get => _iban;
            set => this.RaiseAndSetIfChanged(ref _iban, value);
        }
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
