using SmartTradeLib.Entities;

namespace SmartTradeLib.BusinessLogic
{
    public interface ISmartTradeService
    {
        public void SaveChanges();
        public void RemoveAll();

        public void AddAdmin(Admin admin);
        public void AddCostumer(Costumer costumer);
        public void AddSeller(Seller seller);
    }
}
