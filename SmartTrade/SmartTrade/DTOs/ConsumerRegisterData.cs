using System;
using SmartTrade.Entities;

namespace SmartTradeDTOs;

public class ConsumerRegisterData
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string LastNames { get; set; }
    public string DNI { get; set; }
    public DateTime BirthDate { get; set; }
    public Address BillingAddress { get; set; }
    public Address Address { get; set; }
}