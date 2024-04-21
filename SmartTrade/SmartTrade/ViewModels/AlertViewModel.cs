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
        }

        public async Task LoadNotificationsAsync()
        {
            foreach (var notification in await Service.GetNotificationsAsync())
            {
                ProductsNotifications.Add(new NotificationModel(notification.Post, this, notification));
            }
        }
	}
}