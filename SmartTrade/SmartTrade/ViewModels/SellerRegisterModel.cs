using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.ViewModels
{
    internal class SellerRegisterModel
    {
        public async Task RegisterSeller(string email, string password, string name, string lastnames, string cif, string company, string iban)
        {
            await SmartTradeService.Instance.RegisterSellerAsync(email,password,name,lastnames,cif, company,iban);
        }
    }
}
