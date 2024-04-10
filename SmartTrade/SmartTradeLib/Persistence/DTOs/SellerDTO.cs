using SmartTradeLib.Entities;

namespace SmartTradeDTOs;

public class SellerDTO : UserDTO
{
    public string DNI { get; set; }
    public string CompanyName { get; set; }
    public string IBAN { get; set; }
    public List<int> PostIds { get; set; }

    public SellerDTO(Seller seller) : base(seller)
    {
        DNI = seller.DNI;
        CompanyName = seller.CompanyName;
        IBAN = seller.IBAN;
        PostIds = seller.Posts.Select(p => p.Id).ToList();
    }
}