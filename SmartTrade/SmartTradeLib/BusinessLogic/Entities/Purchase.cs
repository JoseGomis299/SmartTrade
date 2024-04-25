using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        protected Purchase(Product? product, int price, Seller seller, Post? post) : this()
        {
            PurchaseProduct = product;
            Price = price;
            PurchaseSeller = seller;
            PurchasePost = post;
        }
    }
}
