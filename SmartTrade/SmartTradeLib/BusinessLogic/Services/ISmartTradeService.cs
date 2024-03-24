using SmartTradeLib.Entities;

namespace SmartTradeLib.BusinessLogic
{
    public interface ISmartTradeService
    {
        User? Logged { get; set; }
        public void SaveChanges();
        public void RemoveAll();

        public void AddAdmin(Admin admin);
        public void AddCostumer(Consumer costumer);
        public void AddSeller(Seller seller);
        public void AddPost(string? title, string? description, string? productName, Category category, int minimumAge, string? certifications, string? ecologicPrint, List<int> stocks, List<float> prices, List<float> shippingCosts, List<List<byte[]>> images, List<List<string>> attributes);
        public void LogIn(string email, string password);
        public void RegisterSeller(string email, string password, string name, string lastNames, string dni, string companyName, string iban);
        public void RegisterConsumer(string email, string password, string name, string lastNames, string dni, DateTime birthDate, Address billingAddress, Address address);
    }
}
