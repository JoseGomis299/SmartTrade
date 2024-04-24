using SmartTrade.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities
{
    public abstract partial class Purchase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Product? PurchaseProduct { get; set; }
        public int Price { get; set; }
        public Post? PurchasePost { get; set; }
        public Seller? PurchaseSeller { get; set; }
    }
}