using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public class PayPalInfo 
{
    private string paypalemail;
    private string paypalpassword;

    public PayPalInfo(string paypalemail, string paypalpassword)
    {
        this.paypalemail = paypalemail;
        this.paypalpassword = paypalpassword;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}