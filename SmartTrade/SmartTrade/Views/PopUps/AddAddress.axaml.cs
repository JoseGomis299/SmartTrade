using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using SmartTrade.Entities;

namespace SmartTrade.Views
{
    public partial class AddAddress : UserControl
    {
        public Action<bool?> onAccept;
        private bool _hasErrors;
        private int _start = 6;
        public AddAddress()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;

            TextBoxProvince.TextBox.TextChanged += CheckErrors;
            TextBoxStreet.TextBox.TextChanged += CheckErrors;
            TextBoxMunicipality.TextBox.TextChanged += CheckErrors;
            TextBoxPostalCode.TextBox.TextChanged += CheckErrors;
            TextBoxNumber.TextBox.TextChanged += CheckErrors;
            TextBoxDoor.TextBox.TextChanged += CheckErrors;
        }

        public Address GetAddress()
        {
            return new Address(TextBoxProvince.Text, TextBoxStreet.Text, TextBoxMunicipality.Text,
                TextBoxPostalCode.Text, TextBoxNumber.Text, TextBoxDoor.Text);
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            onAccept?.Invoke(SaveCheckBox.IsChecked);
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        private void CheckErrors(object? sender, TextChangedEventArgs e)
        {
            if (--_start >= 0)
            {
                AcceptButton.IsEnabled = false;
                return;
            }
            _hasErrors = false | CheckProvince();
            _hasErrors |= CheckStreet();
            _hasErrors |= CheckMunicipality();
            _hasErrors |= CheckPostalCode();
            _hasErrors |= CheckNumber();
            _hasErrors |= CheckDoor();

            AcceptButton.IsEnabled = !_hasErrors;
        }

        private bool CheckProvince()
        {
            if (TextBoxProvince.Text.IsNullOrEmpty())
            {
                TextBoxProvince.ErrorText = "Please input a province";
                return true;
            }

            TextBoxProvince.ErrorText = "";
            return false;
        }

        private bool CheckStreet()
        {
            if (TextBoxStreet.Text.IsNullOrEmpty())
            {
                TextBoxStreet.ErrorText = "Please input a street";
                return true;
            }

            TextBoxStreet.ErrorText = "";
            return false;
        }

        private bool CheckMunicipality()
        {
            if (TextBoxMunicipality.Text.IsNullOrEmpty())
            {
                TextBoxMunicipality.ErrorText = "Please input a municipality";
                return true;
            }

            TextBoxMunicipality.ErrorText = "";
            return false;
        }

        private bool CheckPostalCode()
        {
            if (TextBoxPostalCode.Text.IsNullOrEmpty())
            {
                TextBoxPostalCode.ErrorText = "Please input a postal code";
                return true;
            }

            if(TextBoxPostalCode.TextBox.Text.Length != 5)
            {
                TextBoxPostalCode.ErrorText = "Invalid postal code. Write at least 5 numbers";
                return true;
            }

            TextBoxPostalCode.ErrorText = "";
            return false;
        }

        private bool CheckNumber()
        {
            if (TextBoxNumber.Text.IsNullOrEmpty())
            {
                TextBoxNumber.ErrorText = "Please input the street number";
                return true;
            }

            TextBoxNumber.ErrorText = "";
            return false;
        }

        private bool CheckDoor()
        {
            if (TextBoxDoor.Text.IsNullOrEmpty())
            {
                TextBoxDoor.ErrorText = "Please input the door number";
                return true;
            }

            TextBoxDoor.ErrorText = "";
            return false;
        }
    }
}

