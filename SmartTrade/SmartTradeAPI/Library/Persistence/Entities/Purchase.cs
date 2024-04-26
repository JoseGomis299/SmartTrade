using SmartTrade.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities
{
    public partial class Purchase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Product? PurchaseProduct { get; set; }
        public float Price { get; set; }
        public float ShippingPrice { get; set; }
        public virtual Post? PurchasePost { get; set; }
        public virtual Seller? PurchaseSeller { get; set; }
        public virtual Offer? PurchaseOffer { get; set;}
    }
}