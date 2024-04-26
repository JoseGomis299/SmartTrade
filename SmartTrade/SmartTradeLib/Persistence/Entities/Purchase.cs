using SmartTrade.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities
{
    public abstract partial class Purchase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Product? PurchaseProduct { get; set; }
        public int Price { get; set; }
        public virtual Post? PurchasePost { get; set; }
        public virtual Seller? PurchaseSeller { get; set; }
        public int PrecioEnvio {  get; set; }   
        public virtual Offer? PurchaseOffer { get; set; }   
    }
}