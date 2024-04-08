using Microsoft.AspNetCore.Mvc;
using SmartTradeAPI.Library.Persistence.NewFolder;
using SmartTradeLib.BusinessLogic;
using SmartTradeLib.Entities;

namespace SmartTradeAPI.Controllers
{
    [ApiController]
    [Route("SmartTradeAPI/Post")]
    public class PostController : ControllerBase
    {
        [HttpGet(Name = "GetAll")]
        public IEnumerable<PostDTO> Get()
        {
            SmartTradeService service = new();
            return service.GetPosts();
        }

        [HttpGet("{id}", Name = "GetPost")]
        public PostDTO Get(int id)
        {
            SmartTradeService service = new();
            return service.GetPost(id);
        }

        [HttpPost(Name = "PublishPost")]
        public void Post([FromBody] PostInfo info)
        {
            SmartTradeService service = new();

            List<int> stocks = new();
            List<float> prices = new();
            List<float> shippingCosts = new();
            List<List<byte[]>> images = new();
            List<List<string>> attributes = new();

            foreach (var offer in info.Offers)
            {
                stocks.Add(offer.stock);
                prices.Add(offer.price);
                shippingCosts.Add(offer.shippingCost);

                foreach (var product in offer.products)
                {
                    images.Add(product.images);
                    attributes.Add(product.attributes);
                }
            }

            service.AddPost(info.Title, info.Description, info.ProductName, info.Category, info.MinimumAge, info.HowToUse, info.Certifications, info.EcologicPrint, info.HowToReducePrint, false, stocks, prices, shippingCosts, images, attributes);
        }

        [HttpPut("{id}", Name = "EditPost")]
        public void Put(int id, [FromBody] PostInfo info)
        {
            SmartTradeService service = new();

            List<int> stocks = new();
            List<float> prices = new();
            List<float> shippingCosts = new();
            List<List<byte[]>> images = new();
            List<List<string>> attributes = new();

            foreach (var offer in info.Offers)
            {
                stocks.Add(offer.stock);
                prices.Add(offer.price);
                shippingCosts.Add(offer.shippingCost);

                foreach (var product in offer.products)
                {
                    images.Add(product.images);
                    attributes.Add(product.attributes);
                }
            }

            service.EditPost(info.Title, info.Description, info.ProductName, info.Category, info.MinimumAge, info.HowToUse, info.Certifications, info.EcologicPrint, info.HowToReducePrint, stocks, prices, shippingCosts, images, attributes, id, info.Validated);
        }

        [HttpDelete("{id}", Name = "RemovePost")]
        public void Delete(int id)
        {
            SmartTradeService service = new();
            service.RejectPost(id);
        }
    }
}
