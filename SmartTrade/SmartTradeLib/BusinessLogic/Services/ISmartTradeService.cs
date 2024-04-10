using SmartTrade.Entities;
using SmartTradeDTOs;

namespace SmartTrade.BusinessLogic
{
    public interface ISmartTradeService
    {
        public void SaveChanges();
        public void RemoveAll();

        public void AddAdmin(Admin admin);
        public void AddCostumer(Consumer costumer);
        public void AddSeller(Seller seller);

        public Post AddPost(string postInfoJson, string loggedID);

        public void EditPost(int postID, string postInfoJson, string loggedID);

        //Returns UserDTO
        public string LogIn(string email, string password);

        //Returns SellerDTO
        public string RegisterSeller(string email, string password, string name, string lastNames, string dni,
            string companyName, string iban);

        //Returns ConsumerDTO
        public string RegisterConsumer(string email, string password, string name, string lastNames, string dni,
            DateTime birthDate, Address billingAddress, Address address);

        public void DeletePost(int postID);

        //Returns List<PostDTO>
        public string GetPosts(string? loggedID);
        //Returns List<PostDTO>
        public string GetPostsFuzzyContain(string searchFor);
        public List<String> GetPostsNamesStartWith(string startWith, int numPosts);
    }
}
