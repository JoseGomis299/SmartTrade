using System.Collections.ObjectModel;

namespace SmartTrade.ViewModels;

public class OrderHistoryModel : ViewModelBase
{
    public ObservableCollection<PurchaseModel> Purchases { get; set; }

    public OrderHistoryModel()
    {
        Purchases = new ObservableCollection<PurchaseModel>();
        
        if (Service.Purchases == null) { return; }

        foreach (var item in Service.Purchases)
        {
            Purchases.Add(new PurchaseModel(item, this));
        }
        
    }

}