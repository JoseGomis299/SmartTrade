using Avalonia.Controls;
using Avalonia.Interactivity;
using SmartTrade.Entities;
using SmartTrade.ViewModels;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartTrade.Views
{
    public partial class SellerRegister : UserControl
    {
        private SellerRegisterModel _model;

        public SellerRegister()
        {
            InitializeComponent();
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

        private async void SignInButton_click(object? sender, RoutedEventArgs e)
        {
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

            try
            {
                Address SellerAddress = new Address(province, street, municipality, postalCode, number, door);
                SmartTradeNavigationManager.Instance.NavigateTo(new SellerCatalog());
                await _model.RegisterSeller(email,password,name,lastnames,cif,company,iban);
            }
            catch(Exception ex) 
            {
                if (ex.Message.Contains("Usuario existente"))
                {
                    TextBoxEmail.ErrorMessage.BringIntoView();
                    TextBoxEmail.ErrorMessage.Text = ex.Message;
                }
            }
            
        }
    }
}
