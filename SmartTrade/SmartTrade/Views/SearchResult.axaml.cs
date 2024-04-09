using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using System.Collections.Generic;
using SmartTradeLib.Entities;

namespace SmartTrade.Views
{
    public partial class SearchResult : UserControl
    {
        private SearchResultModel? _model;

        public SearchResult()
        {
            DataContext = _model = new SearchResultModel();
            InitializeComponent();

            PriceAscendingButton.Click += PriceAscendingButton_Click; ;
            PriceDescendingButton.Click += PriceDescendingButton_Click;
        }

        public SearchResult(List<Post> posts)
        {
            DataContext = _model = new SearchResultModel();
            foreach (var post in posts)
            {
                _model.OriginalSearchedProducts.Add(new ProductModel(post));
                _model.SearchedProducts.Add(new ProductModel(post));
            }
            InitializeComponent();

            PriceAscendingButton.Click += PriceAscendingButton_Click; ;
            PriceDescendingButton.Click += PriceDescendingButton_Click;
            SustainableButton.Click += SustainableButton_Click;
        }

        private void SustainableButton_Click(object? sender, RoutedEventArgs e)
        {
            if (_model.Sustainable) { _model.Sustainable = false; }
            else { _model.Sustainable = true; }

            _model.ApplyFilters();
        }

        private void PriceAscendingButton_Click(object? sender, RoutedEventArgs e)
        {
            if (!_model.PriceAscend)
            {
                _model.PriceAscend = true;
                _model.PriceDescend = false;

                PriceDescendingButton.IsChecked = false;
            }
            else 
            {
                _model.PriceAscend = false;
            }

            _model.ApplyFilters();
        }

        private void PriceDescendingButton_Click(object? sender, RoutedEventArgs e)
        {
            if (!_model.PriceDescend)
            {
                _model.PriceDescend = true;
                _model.PriceAscend = false;

                PriceAscendingButton.IsChecked = false;
            }
            else
            {
                _model.PriceDescend = false;
            }

            _model.ApplyFilters();
        }

        //private void AutoCompleteBox_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        //{
        //    if (e.Equals(Avalonia.Input.Key.Enter)) {

        //        _model.OriginalSearchedProducts.Clear();
        //        _model.SearchedProducts.Clear();
        //        _model.LoadProducts().ForEach(post =>
        //        {
        //            _model.OriginalSearchedProducts.Add(new ProductModel(post));
        //            _model.SearchedProducts.Add(new ProductModel(post));
        //        });
        //    }
        //}

        //private void AutoCompleteBox_TextChanged(object? sender, TextChangedEventArgs e)
        //{
        //    if (_model.SearchText.IsNullOrEmpty()) { _model.SearchAutoComplete.Clear(); return; }
        //    _model.SearchAutoComplete.Clear();
        //    _model.GetNamesProducts().ForEach(Name => {
        //        _model.SearchAutoComplete.Add(Name);
        //    });
        //}
        
    }
}
