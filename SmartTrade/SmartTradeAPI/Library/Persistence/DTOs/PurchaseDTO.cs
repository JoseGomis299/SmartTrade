using SmartTrade.Entities;

namespace SmartTradeAPI.Library.Persistence.DTOs
{
    public class PurchaseDTO
    {
        public float Price { get; set; }
        public float ShippingPrice { get; set; }
        public byte[] Image { get; set; }
        public int? ProductId { get; set; }
        public string? EmailSeller { get; set; }
        public int? PostId { get; set; }
        public int? OfferId { get; set; }

        public PurchaseDTO(){}
        
        public PurchaseDTO(int? idproduct, int? postId, string? emailseller, int? idOffer, float precio, float precioEnvio)
        {
            Price = precio;
            ShippingPrice = precioEnvio;
            ProductId = idproduct;
            EmailSeller = emailseller;
            PostId = postId;
            OfferId = idOffer;
        }
    }
}
