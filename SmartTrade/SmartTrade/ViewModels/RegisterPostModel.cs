using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels
{
    public class RegisterPostModel : PostModificationModel
    {
        public void AddStock()
        {
            bool canAddStock = Category.GetNonRepeatableAttributes().Length != Category.GetAttributes().Length;

            if (Stocks.Count >= 1 && canAddStock)
            {
                Stocks.Add(new Stock(Stocks[0], Category, this));
            }
            else if (Stocks.Count == 0)
            {
                Stocks.Add(new Stock(Category, this));
            }
        }

        public async Task PublishPostAsync()
        {
           PostDTO postDto = CreatePostInfo(null);

           await SmartTradeService.Instance.AddPostAsync(postDto);
           SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
        }
    }
}
