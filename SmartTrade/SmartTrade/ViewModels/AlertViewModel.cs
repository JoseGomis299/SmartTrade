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
		public ObservableCollection<ProductModel> Products { get; set; }
		public AlertViewModel() 
		{
			Products = new ObservableCollection<ProductModel>();
			foreach (var notification in SmartTradeService.Instance.Notifications)
			{
				Products.Add(new ProductModel(notification.Post));
			}
		}
	}
}