using SmartTradeLib.Entities;

namespace SmartTradeLib.BusinessLogic
{
    public interface ISmartTradeService
    {
        public void SaveChanges();
        public void RemoveAll();

        public void AddAdmin(Admin admin);
        public void AddCostumer(Consumer costumer);
        public void AddSeller(Seller seller);
        void AddPost(string? title, string? description, string? productName, Category category, int minimumAge, string? certifications, string? ecologicPrint, List<OfferDTO> offers, List<List<byte[]>> images);
    }
}
