using SmartTrade.Entities;

namespace SmartTradeAPI.Library.Persistence.DTOs
{
    public class PurchaseDTO
    {
        public int Precio { get; set; }
        public int? Idproducto { get; set; }
        public string? EmailSeller { get; set; }
        public int? Idpost { get; set; }

        public PurchaseDTO(){}
        
        public PurchaseDTO(Product product, Post post, Seller seller, int precio)
        {
            this.Precio = precio;
            Idproducto = product.Id;
            EmailSeller = seller.Email;
            Idpost = post.Id;
        }
    }
}
