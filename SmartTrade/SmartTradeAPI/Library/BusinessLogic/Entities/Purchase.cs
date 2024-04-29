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
        public Purchase()
        {
        }

        public Purchase(Product? product, float price, float shippingPrice, Seller seller, Post? post, Offer? purchaseOffer ) : this()
        {
            PurchaseProduct = product;
            Price = price;
            ShippingPrice = shippingPrice;
            PurchaseSeller = seller;
            PurchasePost = post;
            PurchaseOffer = purchaseOffer;
        }
    }
}
