using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.Entities;
using SmartTrade.ViewModels;
using System;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartTrade.Views
{
    public partial class SellerRegister : UserControl
    {
        private SellerRegisterModel _model;

        public SellerRegister()
        {
            InitializeComponent();
            DataContext = new SellerRegisterModel();
            SignInButton.Click += SignInButton_click;
            RegisterConsumerButton.Click += RegisterConsumerButton_click;
            LogInButton.Click += LoginButton_click;
        }

        private void LoginButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new Login());
        }

        private void RegisterConsumerButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new Register());
        }
        private void ClearErrors()
        {
            TextBoxName.ErrorText = "";
            TextBoxEmail.ErrorText = "";
            TextBoxLastNames.ErrorText = "";
            TextBoxPassword.ErrorText = "";
            TextBoxCIF.ErrorText = "";
            TextBoxNumber.ErrorText = "";
            TextBoxIBAN.ErrorText = "";
            TextBoxCompany.ErrorText = "";
        }
        private async void SignInButton_click(object? sender, RoutedEventArgs e)
        {
            ClearErrors();
            string name = TextBoxName.Text;
            string lastnames = TextBoxLastNames.Text;
            string email = TextBoxEmail.Text;
            string password = TextBoxPassword.Text;
            string cif = TextBoxCIF.Text;
            string company = TextBoxCompany.Text;
            string province = TextBoxProvince.Text;
            string municipality = TextBoxMunicipality.Text;
            string postalCode = TextBoxPostalCode.Text;
            string street = TextBoxStreet.Text;
            string number = TextBoxNumber.Text;
            string door = TextBoxDoor.Text;
            string iban = TextBoxIBAN.Text;
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
            if (_model.CIF.IsNullOrEmpty())
            {
                TextBoxCIF.BringIntoView();
                TextBoxCIF.Focus();
                TextBoxCIF.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.Company.IsNullOrEmpty())
            {
                TextBoxCompany.BringIntoView();
                TextBoxCompany.Focus();
                TextBoxCompany.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.IBAN.IsNullOrEmpty())
            {
                TextBoxIBAN.BringIntoView();
                TextBoxIBAN.Focus();
                TextBoxIBAN.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.LastNames.IsNullOrEmpty())
            {
                TextBoxLastNames.BringIntoView();
                TextBoxLastNames.Focus();
                TextBoxLastNames.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            try
            {
                if (hasErrors) return;
                _model.ValidarDniCif(cif);
                Address SellerAddress = new Address(province, street, municipality, postalCode, number, door);
                await _model.RegisterSeller(email,password,name,lastnames,cif,company,iban);
                SmartTradeNavigationManager.Instance.NavigateTo(new SellerCatalog());
            }
            catch(Exception ex) 
            {
                if (ex.Message.Contains("Existing user"))
                {
                    TextBoxEmail.ErrorMessage.BringIntoView();
                    TextBoxEmail.ErrorMessage.Text = ex.Message;
                }

                if (ex.Message.Contains("Incorrect CIF/DNI. Please enter a valid CIF/DNI"))
                {
                    TextBoxCIF.ErrorMessage.BringIntoView();
                    TextBoxCIF.ErrorMessage.Text = ex.Message;
                }
            }
            
        }
    }
}
