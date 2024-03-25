﻿using System.Collections.Generic;
using System.Linq;

namespace SmartTrade.ViewModels
{
    public class RegisterPostModel : PostModificationModel
    {
        public void PublishPost()
        {
            List<int> stocks = new();
            List<float> prices = new();
            List<float> shippingCosts = new();

            List<List<byte[]>> images = new();
            List<List<string>> attributes = new();

            foreach (var stock in Stocks)
            {
                images.Add(stock.Images.Select(image => image.Bytes).ToList());

                stocks.Add(int.Parse(stock.StockQuantity));
                prices.Add(float.Parse(stock.Price));
                shippingCosts.Add(float.Parse(stock.ShippingCost));

                attributes.Add(stock.CategoryAttributes.Select(attribute => attribute.Value).ToList());
            }

            MainViewModel.SmartTradeService.AddPost(Title, Description, ProductName, Category, int.Parse(MinimumAge), Certifications, EcologicPrint, stocks, prices, shippingCosts, images, attributes);
        }
    }
}
