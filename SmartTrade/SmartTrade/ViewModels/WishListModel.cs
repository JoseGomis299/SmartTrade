using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace SmartTrade.ViewModels
{
	public class WishListModel : ReactiveObject
	{
        public ObservableCollection<NotificationModel> ProductsInWishList { get; set; }

        public WishListModel() 
        {
            ProductsInWishList = new ObservableCollection<NotificationModel>();
        }


    }
}