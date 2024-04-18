using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI;
using SmartTrade.Entities;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels
{
	public class AlertViewModel : ReactiveObject
	{
		public ObservableCollection<ProductModel> ProductsNotifications { get; set; }
		public AlertViewModel() 
		{
            ProductsNotifications = new ObservableCollection<ProductModel>();
		}

        public async Task LoadNotificationsAsync()
        {
			if(SmartTradeService.Instance.Notifications == null)
				await SmartTradeService.Instance.GetNotificationsAsync();

            foreach (var notification in SmartTradeService.Instance.Notifications)
            {
                ProductsNotifications.Add(new ProductModel(notification.Post));
            }
        }

		//public AlertDTO GetAlert()
		//{
			
		//}
	}
}