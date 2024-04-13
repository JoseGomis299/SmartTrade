using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using ReactiveUI;
using SmartTrade.Views;
using SmartTradeDTOs;
using SmartTrade.Entities;

namespace SmartTrade.ViewModels
{
	public class ProductViewModel : ReactiveObject
	{
        private MainViewModel _mainViewModel;
        public PostDTO post;

        public ProductViewModel(PostDTO post)
        {
            this.post = post;
        }

        private void ShowShoppingCart()
        {
            _mainViewModel.CartVisible = true;
            _mainViewModel.ButtonVisible = false;
        }
    }
}