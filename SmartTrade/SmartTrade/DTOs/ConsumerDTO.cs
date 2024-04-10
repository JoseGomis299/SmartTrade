using System;
using System.Collections.Generic;
using SmartTrade.Entities;

namespace SmartTradeDTOs;

public class ConsumerDTO : UserDTO
{
    public string DNI { get; set; }
    public DateTime BirthDate { get; set; }

    public Address BillingAddress { get; set; }
    public List<Address> Addresses { get; set; }
    public List<PayPalInfo> PayPalAccounts { get; set; }
    public List<BizumInfo> BizumAccounts { get; set; }
    public List<CreditCardInfo> CreditCards { get; set; }
    public List<AlertDTO> Alerts { get; set; }
}