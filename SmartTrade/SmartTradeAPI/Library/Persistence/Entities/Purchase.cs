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
        public int Quantity { get; set; }
        public virtual Post? Post { get; set; }
        public virtual Seller? PurchaseSeller { get; set; }
        public virtual Offer? Offer { get; set;}
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpectedDate { get; set; }
        public virtual Address? DeliveryAddress { get; set; }
        public virtual Address? BillingAddress { get; set; }
    }
}