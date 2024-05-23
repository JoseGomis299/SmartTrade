using System;
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
            await Service.RegisterSellerAsync(Email, Password, Name, LastNames,CIF, Company, IBAN);
        }
    }
}
