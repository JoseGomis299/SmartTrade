using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using SmartTrade.Entities;

namespace SmartTrade.ViewModels
{
	public class AlertViewModel : ReactiveObject
	{
		public ObservableCollection<NotificationModel> ProductsNotifications { get; set; }

        public AlertViewModel() 
		{
            ProductsNotifications = new ObservableCollection<NotificationModel>();
		}

        public async Task LoadNotificationsAsync()
        {
			if(SmartTradeService.Instance.Notifications == null)
				await SmartTradeService.Instance.GetNotificationsAsync();

            foreach (var notification in SmartTradeService.Instance.Notifications)
            {
                ProductsNotifications.Add(new NotificationModel(notification.Post, this, notification));
            }
        }
	}
}