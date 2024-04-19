using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using SmartTrade;
using SmartTrade.Entities;
using Avalonia.Input;
using System.Linq;
using SmartTrade.ViewModels;
using Splat;
using Microsoft.IdentityModel.Tokens;

namespace SmartTrade.Views
{
    public partial class Register : UserControl
    {
      
        private RegisterModel _model;

        public Register()
        {
            DataContext = _model = new RegisterModel();

            InitializeComponent();
            SignInButton.Click += SignInButton_click;
            RegisterSellerButton.Click += RegisterSellerButton_click;
            LoginButton.Click += LoginButton_click;
            CreditCardButton.Click += CreditCardButton_click;
            BizumButton.Click += BizumButton_click;
            PaypalButton.Click += PaypalButton_click;
            BirthDateError.IsVisible = false;
            PaypalButton.IsVisible = true;
            CreditCardButton.IsVisible = true;
            BizumButton.IsVisible = true;
            Paypaladd.IsVisible = false;
            Bizumadd.IsVisible = false;
            CreditCardadd.IsVisible = false;
        }

        public void CreditCardAdded()
        {
            if (!_model.CreditCardName.IsNullOrEmpty() && !_model.CreditCardNumber.IsNullOrEmpty() && !_model.CreditCardCVV.IsNullOrEmpty() && !_model.CreditCardExpiryDate.IsNullOrEmpty())
            {
                CreditCardButton.IsVisible = false;
                CreditCardadd.IsVisible = true;

            }
        }

        public void BizumAdded()
        {
            if (!_model.BizumNumber.IsNullOrEmpty())
            {
                BizumButton.IsVisible = false;
                Bizumadd.IsVisible = true;

            }
        }

        public void PaypalAdded()
        {
            if (!_model.PaypalEmail.IsNullOrEmpty() && !_model.PaypalPassword.IsNullOrEmpty())
            {
                PaypalButton.IsVisible = false;
                Paypaladd.IsVisible = true;

            }
        }

        private void PaypalButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new Paypal(_model, PaypalAdded));
        }

        private void BizumButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new Bizum(_model, BizumAdded));
        }

        private void CreditCardButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new AddCreditCart(_model, CreditCardAdded));
        }

        private void LoginButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new Login());
        }

        private void RegisterSellerButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new SellerRegister());
        }

        private void ClearErrors()
        {
            TextBoxName.ErrorText = "";
            TextBoxEmail.ErrorText = "";
            TextBoxLastNames.ErrorText = "";
            TextBoxPassword.ErrorText = "";
            TextBoxDNI.ErrorText = "";
            TextBoxNumber.ErrorText = "";
            TextBoxProvince.ErrorText = "";
            TextBoxPostalCode.ErrorText = "";
            TextBoxMunicipality.ErrorText = "";
            TextBoxStreet.ErrorText = "";
            TextBoxDoor.ErrorText = "";
            BirthDateError.IsVisible = false;

        }
        private async void SignInButton_click(object? sender, RoutedEventArgs e)
        {
            ClearErrors();
            bool hasErrors = false;
            if (_model.Name.IsNullOrEmpty())
            {
                TextBoxName.BringIntoView();
                TextBoxName.Focus();
                TextBoxName.ErrorText = "Name cannot be empty";
                hasErrors = true;
            }
            if (_model.Email.IsNullOrEmpty())
            {
                TextBoxEmail.BringIntoView();
                TextBoxEmail.Focus();
                TextBoxEmail.ErrorText = "Email cannot be empty";
                hasErrors = true;
            }
            if (_model.Password.IsNullOrEmpty())
            {
                TextBoxPassword.BringIntoView();
                TextBoxPassword.Focus();
                TextBoxPassword.ErrorText = "Password cannot be empty";
                hasErrors = true;
            }
            if (_model.DNI.IsNullOrEmpty())
            {
                TextBoxDNI.BringIntoView();
                TextBoxDNI.Focus();
                TextBoxDNI.ErrorText = "DNI cannot be empty";
                hasErrors = true;
            }
            if (_model.DateBirth == null)
            {
                BirthDateError.IsVisible = true;
                hasErrors = true;
            }
            if (_model.LastNames.IsNullOrEmpty())
            {
                TextBoxLastNames.BringIntoView();
                TextBoxLastNames.Focus();
                TextBoxLastNames.ErrorText = "Last Names cannot be empty";
                hasErrors = true;
            }
            if (_model.Province.IsNullOrEmpty())
            {
                TextBoxProvince.BringIntoView();
                TextBoxProvince.Focus();
                TextBoxProvince.ErrorText = "Province cannot be empty";
                hasErrors = true;
            }
            if (_model.PostalCode.IsNullOrEmpty())
            {
                TextBoxPostalCode.BringIntoView();
                TextBoxPostalCode.Focus();
                TextBoxPostalCode.ErrorText = "Postal Code cannot be empty";
                hasErrors = true;
            }
            if (_model.Municipality.IsNullOrEmpty())
            {
                TextBoxMunicipality.BringIntoView();
                TextBoxMunicipality.Focus();
                TextBoxMunicipality.ErrorText = "Municipality cannot be empty";
                hasErrors = true;
            }
            if (_model.Street.IsNullOrEmpty())
            {
                TextBoxStreet.BringIntoView();
                TextBoxStreet.Focus();
                TextBoxStreet.ErrorText = "Street cannot be empty";
                hasErrors = true;
            }
            if (_model.Number.IsNullOrEmpty())
            {
                TextBoxNumber.BringIntoView();
                TextBoxNumber.Focus();
                TextBoxNumber.ErrorText = "Number cannot be empty";
                hasErrors = true;
            }
            if (_model.Door.IsNullOrEmpty())
            {
                TextBoxDoor.BringIntoView();
                TextBoxDoor.Focus();
                TextBoxDoor.ErrorText = "Door cannot be empty";
                hasErrors = true;
            }
            try
            {
                if (hasErrors) return;
                await _model.RegisterConsumer();
                await SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("You cannot select future dates"))
                {
                    BirthDateError.IsVisible = true;
                    hasErrors = true;

                }
                if (ex.Message.Contains("Existing user"))
                {
                    TextBoxEmail.BringIntoView();
                    TextBoxEmail.ErrorText = ex.Message;
                }
                if (ex.Message.Contains("Incorrect DNI. Must be 8 digits followed by a letter"))
                {
                    TextBoxDNI.BringIntoView();
                    TextBoxDNI.ErrorText = ex.Message;
                }
                if (ex.Message.Contains("Wrong email. Please enter a valid email"))
                {
                    TextBoxEmail.BringIntoView();
                    TextBoxEmail.ErrorText = ex.Message;
                }
                if (ex.Message.Contains("Wrong phone number. Only digits are allowed"))
                {
                    TextBoxNumber.BringIntoView();
                    TextBoxNumber.ErrorText = ex.Message;
                }
            }
        }
       
    }
}
