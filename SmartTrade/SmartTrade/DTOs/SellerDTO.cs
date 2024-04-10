using System.Collections.Generic;
using SmartTrade.Entities;

namespace SmartTradeDTOs;

public class SellerDTO : UserDTO
{
    public string DNI { get; set; }
    public string CompanyName { get; set; }
    public string IBAN { get; set; }
    public List<int> PostIds { get; set; }
}