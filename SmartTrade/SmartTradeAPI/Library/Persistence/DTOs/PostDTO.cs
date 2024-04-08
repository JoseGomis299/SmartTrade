using SmartTradeLib.Entities;

namespace SmartTradeAPI.Library.Persistence.NewFolder
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Validated { get; set; }
        public string SellerID { get; set; }
        public string SellerCompanyName { get; set; }
        public ICollection<int> OffersIDs { get; set; }
        public float Price { get; set; }

        public PostDTO(Post post){
            Id = post.Id;
            Title = post.Title;
            Description = post.Description;
            Validated = post.Validated;
            SellerID = post.Seller.Email;
            SellerCompanyName = post.Seller.CompanyName;
            OffersIDs = post.Offers.Select(offer => offer.Id).ToList();
            Price = post.Offers.First().Price;
        }
    }
}
