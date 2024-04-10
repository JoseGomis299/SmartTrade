using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.ViewModels
{
    public class RegisterModel : ViewModelBase
    {
        public DateTime convertDate(string dateString)
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
    }        
}
