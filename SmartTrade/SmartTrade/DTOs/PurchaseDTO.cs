using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.DTOs
{
    public class PurchaseDTO
    {
        public int Precio { get; set; }
        public int? Idproducto { get; set; }
        public string? EmailSeller { get; set; }
        public int? Idpost { get; set; }

        public PurchaseDTO() { }

        public PurchaseDTO(Product product, Post post, Seller seller, int precio)
        {
            this.Precio = precio;
            Idproducto = product.Id;
            EmailSeller = seller.Email;
            Idpost = post.Id;
        }
    }
}
