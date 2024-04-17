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

        }

        private void PaypalButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new Paypal(_model));
        }

        private void BizumButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new Bizum(_model));
        }

        private void CreditCardButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new AddCreditCart(_model));
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
            TextBoxDateBirth.ErrorText = "";
            TextBoxNumber.ErrorText = "";
            TextBoxProvince.ErrorText = "";
            TextBoxPostalCode.ErrorText = "";
            TextBoxMunicipality.ErrorText = "";
            TextBoxStreet.ErrorText = "";
            TextBoxDoor.ErrorText = "";

        }
        private async void SignInButton_click(object? sender, RoutedEventArgs e)
        {
            ClearErrors();
            bool hasErrors = false;
            if (_model.Name.IsNullOrEmpty())
            {
                TextBoxName.BringIntoView();
                TextBoxName.Focus();
                TextBoxName.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.Email.IsNullOrEmpty())
            {
                TextBoxEmail.BringIntoView();
                TextBoxEmail.Focus();
                TextBoxEmail.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.Password.IsNullOrEmpty())
            {
                TextBoxPassword.BringIntoView();
                TextBoxPassword.Focus();
                TextBoxPassword.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.DNI.IsNullOrEmpty())
            {
                TextBoxDNI.BringIntoView();
                TextBoxDNI.Focus();
                TextBoxDNI.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.DateBirth.IsNullOrEmpty())
            {
                TextBoxDateBirth.BringIntoView();
                TextBoxDateBirth.Focus();
                TextBoxDateBirth.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.LastNames.IsNullOrEmpty())
            {
                TextBoxLastNames.BringIntoView();
                TextBoxLastNames.Focus();
                TextBoxLastNames.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.Province.IsNullOrEmpty())
            {
                TextBoxProvince.BringIntoView();
                TextBoxProvince.Focus();
                TextBoxProvince.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.PostalCode.IsNullOrEmpty())
            {
                TextBoxPostalCode.BringIntoView();
                TextBoxPostalCode.Focus();
                TextBoxPostalCode.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.Municipality.IsNullOrEmpty())
            {
                TextBoxMunicipality.BringIntoView();
                TextBoxMunicipality.Focus();
                TextBoxMunicipality.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.Street.IsNullOrEmpty())
            {
                TextBoxStreet.BringIntoView();
                TextBoxStreet.Focus();
                TextBoxStreet.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.Number.IsNullOrEmpty())
            {
                TextBoxNumber.BringIntoView();
                TextBoxNumber.Focus();
                TextBoxNumber.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.Door.IsNullOrEmpty())
            {
                TextBoxDoor.BringIntoView();
                TextBoxDoor.Focus();
                TextBoxDoor.ErrorText = "Title cannot be empty";
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
                if (ex.Message.Contains("Incorrect format"))
                {
                    TextBoxDateBirth.ErrorMessage.BringIntoView();
                    TextBoxDateBirth.ErrorMessage.Text = ex.Message;
                }
                if (ex.Message.Contains("Existing user"))
                {
                    TextBoxEmail.ErrorMessage.BringIntoView();
                    TextBoxEmail.ErrorMessage.Text = ex.Message;
                }
                if (ex.Message.Contains("Incorrect DNI. Must be 8 digits followed by a letter"))
                {
                    TextBoxDNI.ErrorMessage.BringIntoView();
                    TextBoxDNI.ErrorMessage.Text = ex.Message;
                }
                if (ex.Message.Contains("Wrong email. Please enter a valid email"))
                {
                    TextBoxEmail.ErrorMessage.BringIntoView();
                    TextBoxEmail.ErrorMessage.Text = ex.Message;
                }
                if (ex.Message.Contains("Wrong phone number. Only digits are allowed"))
                {
                    TextBoxNumber.ErrorMessage.BringIntoView();
                    TextBoxNumber.ErrorMessage.Text = ex.Message;
                }
            }
        }
        private void DateTextBox_TextInput(object sender, TextInputEventArgs e)
        {
            var textBox = (TextBox)sender;
            var text = textBox.Text;
            var caretIndex = textBox.CaretIndex;
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
            text = new string(text.Where(c => char.IsDigit(c)).ToArray()).PadRight(8, '0');
            if (text.Length >= 2)
            {
                text = text.Insert(2, "/");
            }
            if (text.Length >= 5)
            {
                text = text.Insert(5, "/");
            }
            textBox.Text = text;
            textBox.CaretIndex = Math.Min(caretIndex + 1, text.Length);
            e.Handled = true;
        }
    }
}
