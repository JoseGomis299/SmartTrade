using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.BusinessLogic
{
    public static class Extensions
    {
        public static string ToCommonSyntax(this string str)
        {
            return str.ToLower().Trim();
        }
    }
}
