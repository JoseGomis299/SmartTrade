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
using Avalonia.VisualTree;
using System.Net;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using Avalonia.LogicalTree;
using Avalonia.Controls.Templates;
using System.Reflection;
using Avalonia.Controls.Primitives;

namespace SmartTrade.Views
{
    public partial class Register : UserControl
    {
      
        private RegisterModel _model;
        private bool _hasErrors;
        private int _startUserInfo = 6;
        private int _startAddressInfo = 6;
        private int _start = 12;

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

            TextBoxName.TextBox.TextChanged += CheckUserErrors;
            TextBoxLastNames.TextBox.TextChanged += CheckUserErrors;
            TextBoxDNI.TextBox.TextChanged += CheckUserErrors;
            TextBoxEmail.TextBox.TextChanged += CheckUserErrors;
            TextBoxDateBirth.TemplateApplied += SetComponentsCalendarDatePicker;
            TextBoxPassword.TextBox.TextChanged += CheckUserErrors;

            TextBoxProvince.TextBox.TextChanged += CheckAddressErrors;
            TextBoxMunicipality.TextBox.TextChanged += CheckAddressErrors;
            TextBoxPostalCode.TextBox.TextChanged += CheckAddressErrors;
            TextBoxStreet.TextBox.TextChanged += CheckAddressErrors;
            TextBoxNumber.TextBox.TextChanged += CheckAddressErrors;
            TextBoxDoor.TextBox.TextChanged += CheckAddressErrors;
        }

        private void SetComponentsCalendarDatePicker(object? sender, Avalonia.Controls.Primitives.TemplateAppliedEventArgs e)
        {
            var textBoxField = sender.GetType().GetField("_textBox", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            TextBox textBox = (TextBox) textBoxField.GetValue(sender);
            textBox.TextChanged += CheckUserErrors;

            TextBoxDateBirth.BlackoutDates.Add(new CalendarDateRange(DateTime.Today, DateTime.MaxValue));
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

        #region ErrorChecking

        private void CheckUserErrors(object? sender, TextChangedEventArgs e)
        {
            if (--_start >= 0)
            {
                SignInButton.IsEnabled = false;
                return;
            }
            _hasErrors = false | CheckName();
            _hasErrors |= CheckLastName();
            _hasErrors |= CheckDNI();
            _hasErrors |= CheckEmail();
            _hasErrors |= CheckBirthDate();
            _hasErrors |= CheckPassword();

            SignInButton.IsEnabled = !_hasErrors;
        }

        private bool CheckName()
        {
            if (TextBoxName.Text.IsNullOrEmpty())
            {
                TextBoxName.ErrorText = "Please input your name";
                return true;
            }

            TextBoxName.ErrorText = "";
            return false;
        }

        private bool CheckLastName()
        {
            if (TextBoxLastNames.Text.IsNullOrEmpty())
            {
                TextBoxLastNames.ErrorText = "Please input your last names";
                return true;
            }

            TextBoxLastNames.ErrorText = "";
            return false;
        }

        private bool CheckDNI()
        {
            if (TextBoxDNI.Text.IsNullOrEmpty())
            {
                TextBoxDNI.ErrorText = "Please input your DNI";
                return true;
            }

            string pattern = @"^\d{8}[A-Za-z]$";
            if (!Regex.IsMatch(TextBoxDNI.Text, pattern))
            {
                TextBoxDNI.ErrorText = ("Incorrect DNI. Must be 8 digits followed by a letter");
                return true;
            }

            TextBoxDNI.ErrorText = "";
            return false;
        }

        private bool CheckEmail()
        {
            if (TextBoxEmail.Text.IsNullOrEmpty())
            {
                TextBoxEmail.ErrorText = "Please input your Email";
                return true;
            }

            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            if (!Regex.IsMatch(TextBoxEmail.Text, pattern))
            {
                TextBoxEmail.ErrorText = "Invalid email. Please enter a valid email";
            }

            TextBoxEmail.ErrorText = "";
            return false;
        }

        private bool CheckBirthDate()
        {
            string date = TextBoxDateBirth.FindDescendantOfType<TextBox>().Text;

            if (date.IsNullOrEmpty())
            {
                BirthDateError.Text = "Please input your birth date";
                BirthDateError.IsVisible = true;
                return true;
            }

            string pattern = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$";
            if (!Regex.IsMatch(TextBoxDateBirth.Text, pattern))
            {
                BirthDateError.Text = "Invalid format. Please enter a date with the format dd/MM/yyyy";
                return true;
            }

            BirthDateError.Text = "";
            BirthDateError.IsVisible = false;
            return false;
        }

        private bool CheckPassword()
        {
            if (TextBoxPassword.Text.IsNullOrEmpty())
            {
                TextBoxPassword.ErrorText = "Please input a password";
                return true;
            }

            TextBoxPassword.ErrorText = "";
            return false;
        }

        private void CheckAddressErrors(object? sender, TextChangedEventArgs e)
        {
            if (--_start >= 0)
            {
                SignInButton.IsEnabled = false;
                return;
            }
            _hasErrors = false | CheckProvince();
            _hasErrors |= CheckMunicipality();
            _hasErrors |= CheckPostalCode();
            _hasErrors |= CheckStreet();
            _hasErrors |= CheckNumber();
            _hasErrors |= CheckDoor();

            SignInButton.IsEnabled = !_hasErrors;
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

            if (TextBoxPostalCode.TextBox.Text.Length != 5)
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

        #endregion

        private async void SignInButton_click(object? sender, RoutedEventArgs e)
        {
            await _model.RegisterConsumer();
            await SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
        }
       
    }
}
