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
			foreach (var notification in SmartTradeService.Instance.Notifications)
			{
                ProductsNotifications.Add(new ProductModel(notification.Post));
			}
		}

		public async Task LoadNotificationsAsync()
		{
            SmartTradeService service = new SmartTradeService();
            List<NotificationDTO> notifications = await service.GetNotificationsAsync(service.Logged.Email);

			foreach (var post in notifications)
			{
				ProductsNotifications.Add(new ProductModel(post.Post));
			}
        }
	}
}