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
        public ObservableCollection<WishLModel> ProductsInWishList { get; set; }
        public UserDTO User;


        public WishListModel() 
        {
            ProductsInWishList = new ObservableCollection<WishLModel>();
        }

        public async Task LoadWishListAsync(string userId)
        {
            foreach (var wish in await Service.GetWishAsync())
            {
                ProductsInWishList.Add(new WishLModel(wish.Post, this, wish));
            }
        }
    }
}