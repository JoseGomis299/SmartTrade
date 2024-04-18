using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	}
}