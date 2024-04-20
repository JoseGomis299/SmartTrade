using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using SmartTrade.Entities;
using SmartTrade.Services;

namespace SmartTrade.ViewModels
{
    public class AlertViewModel : ViewModelBase
	{
		public ObservableCollection<NotificationModel> ProductsNotifications { get; set; }

        public AlertViewModel() 
		{
            ProductsNotifications = new ObservableCollection<NotificationModel>();

            LoadNotifications();
        }

        public void LoadNotifications()
        {
            foreach (var notification in Proxy.Notifications)
            {
                ProductsNotifications.Add(new NotificationModel(notification.Post, this, notification));
            }
        }
	}
}