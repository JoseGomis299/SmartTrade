using System.Collections.Generic;
using System.Linq;

namespace SmartTrade.ViewModels
{
    public class RegisterPostModel : PostModificationModel
    {
        public void AddStock()
        {
            bool canAddStock = Category.GetNonRepeatableAttributes().Length == Category.GetAttributes().Length;

            if (Stocks.Count >= 1)
            {
                Stocks.Add(new Stock(Stocks[0], Category, this));
            }
            else if(canAddStock)
            {
                Stocks.Add(new Stock(Category, this));
            }
        }

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
