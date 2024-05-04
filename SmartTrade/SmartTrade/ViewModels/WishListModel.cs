using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SmartTrade.Services;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels
{
	public class WishListModel : ViewModelBase
    {
        public ObservableCollection<WishModel> ProductsInWishList { get; set; }
        public UserDTO User;


        public WishListModel()
        {
            ProductsInWishList = new ObservableCollection<WishModel>();
        }

        public async Task LoadWishListAsync()
        {
            ProductsInWishList.Clear();
            foreach (var wish in await Service.GetWishAsync())
            {
                ProductsInWishList.Add(new WishModel(wish.Post, this, wish));
            }
        }
    }
}