using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartTradeDTOs;

namespace SmartTrade.Entities
{
    public partial class CartItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Post Post { get; set; }
        public virtual Offer Offer { get; set; }
        public int Quantity { get; set; }
    }
}
