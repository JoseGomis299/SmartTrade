﻿using SmartTrade.Entities;

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
    public List<Alert> Alerts { get; set; }
    public List<Post> WishList { get; set; }

    public ConsumerDTO() : base()
    {
        DNI = "";
        BirthDate = new DateTime();

        BillingAddress = new Address();
        Addresses = new List<Address>();
        PayPalAccounts = new List<PayPalInfo>();
        BizumAccounts = new List<BizumInfo>();
        CreditCards = new List<CreditCardInfo>();
        Alerts = new List<Alert>();
        WishList = new List<Post>();
    }

    public ConsumerDTO(Consumer consumer) : base(consumer)
    {
        DNI = consumer.DNI;
        BirthDate = consumer.BirthDate;

        BillingAddress = consumer.BillingAddress;
        Addresses = consumer.Addresses.ToList();
        PayPalAccounts = consumer.PayPalAccounts.ToList();
        BizumAccounts = consumer.BizumAccounts.ToList();
        CreditCards = consumer.CreditCards.ToList();
        Alerts = consumer.Alerts.ToList();
        WishList = consumer.WishList.ToList();
    }
}