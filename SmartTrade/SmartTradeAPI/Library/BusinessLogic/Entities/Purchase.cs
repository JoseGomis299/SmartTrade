using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTrade.BusinessLogic;
using SmartTrade.Entities;

namespace SmartTrade.Entities
{
    public partial class Purchase
    {
        protected Purchase()
        {
        }

        protected Purchase(Product? product, int price, int shippingPrice, Seller seller, Post? post ) : this()
        {
            PurchaseProduct = product;
            Price = price;
            ShippingPrice = shippingPrice;
            PurchaseSeller = seller;
            PurchasePost = post;
        }
    }
}
