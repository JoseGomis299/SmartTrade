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
        public int PrecioEnvio {  get; set; } 
        public int? Idoffer { get; set; }    

        public PurchaseDTO() { }

        public PurchaseDTO(int? idproduct, int? idpost, string? emailseller, int precio, int precioEnvio, int? idOffer)
        {
            this.Precio = precio;
            Idproducto = idproduct;
            EmailSeller = emailseller;
            Idpost = idpost;
            PrecioEnvio = precioEnvio;
            Idoffer = idOffer;
        }
    }
}
