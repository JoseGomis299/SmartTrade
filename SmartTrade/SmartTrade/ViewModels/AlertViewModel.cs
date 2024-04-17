using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels
{
	public class AlertViewModel : ReactiveObject
	{
		public ObservableCollection<ProductModel> ProductsNotifications { get; set; }
		public string ConsumerId { get; set; }
		public AlertViewModel() 
		{
			ProductsNotifications = new ObservableCollection<ProductModel>();
			ConsumerId = SmartTradeService.Instance.Logged.Email;
		}

		public void GetAlerts()
		{
			
		}
	}
}