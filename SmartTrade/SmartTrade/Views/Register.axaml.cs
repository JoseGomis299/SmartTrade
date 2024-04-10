using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using SmartTradeLib;
using SmartTradeLib.Entities;
using Avalonia.Input;
using System.Linq;
using SmartTrade.ViewModels;
using Splat;

namespace SmartTrade.Views
{
    public partial class Register : UserControl
    {
        private RegisterModel _model;

        public Register()
        {
            InitializeComponent();
            SignInButton.Click += SignInButton_click;
            RegisterSellerButton.Click += RegisterSellerButton_click;
            LoginButton.Click += LoginButton_click;

        }

        private void LoginButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new Login());
        }

        private void RegisterSellerButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new SellerRegister());
        }

        private void SignInButton_click(object? sender, RoutedEventArgs e)
        {
            string name = TextBoxName.Text;
            string lastnames = TextBoxLastNames.Text;
            string email = TextBoxEmail.Text;
            string password = TextBoxPassword.Text;
            string dni = TextBoxDNI.Text;
            string province = TextBoxProvince.Text;
            string municipality = TextBoxMunicipality.Text;
            string postalCode = TextBoxPostalCode.Text;
            string street = TextBoxStreet.Text;
            string number = TextBoxNumber.Text;
            string door = TextBoxDoor.Text;
            string dateBirthString = TextBoxDateBirth.Text;
            try
            {
                Address consumerAddress = new Address(province, street, municipality, postalCode, number, door);
                DateTime dateBirth = _model.convertDate(dateBirthString);
                _model.RegisterConsumer(email,password,name,lastnames,dni,dateBirth,consumerAddress,consumerAddress);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Formato Incorrecto"))
                {
                    TextBoxDateBirth.ErrorMessage.BringIntoView();
                    TextBoxDateBirth.ErrorMessage.Text = ex.Message;
                }
                if (ex.Message.Contains("Usuario existente"))
                {
                    TextBoxEmail.ErrorMessage.BringIntoView();
                    TextBoxEmail.ErrorMessage.Text = ex.Message;
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
