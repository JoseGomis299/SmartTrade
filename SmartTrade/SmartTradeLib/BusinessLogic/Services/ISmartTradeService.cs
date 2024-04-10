using SmartTradeDTOs;
using SmartTradeLib.Entities;

namespace SmartTradeLib.BusinessLogic
{
    public interface ISmartTradeService
    {
        public User? Logged { get; set; }
        public void SaveChanges();
        public void RemoveAll();

        public void AddAdmin(Admin admin);
        public void AddCostumer(Consumer costumer);
        public void AddSeller(Seller seller);

        public Post AddPost(string postInfoJson);

        public void EditPost(int postID, string postInfoJson);

        public void LogIn(string email, string password);

        public void RegisterSeller(string email, string password, string name, string lastNames, string dni,
            string companyName, string iban);

        public void RegisterConsumer(string email, string password, string name, string lastNames, string dni,
            DateTime birthDate, Address billingAddress, Address address);

        public void DeletePost(int postID);

        //Returns List<PostDTO>
        public string GetPosts();
        //Returns List<PostDTO>
        public string GetPostsFuzzyContain(string searchFor);
        public List<String> GetPostsNamesStartWith(string startWith, int numPosts);
    }
}
