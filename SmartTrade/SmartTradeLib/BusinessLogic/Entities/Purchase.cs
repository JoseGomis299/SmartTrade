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

        public Purchase(Product? product, float price, float shippingPrice, int quantity, Seller seller, Post? post, Offer? purchaseOffer, DateTime purchaseDate, DateTime expectedDate) : this()
        {
            PurchaseProduct = product;
            Price = price;
            ShippingPrice = shippingPrice;
            Quantity = quantity;
            PurchaseSeller = seller;
            Post = post;
            Offer = purchaseOffer;
            PurchaseDate = purchaseDate;
            ExpectedDate = expectedDate;
        }
    }
}
