using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using System.Linq;

namespace SmartTrade.Views
{
    public partial class AddCreditCart : UserControl
    {
        private RegisterModel? _model;

        public AddCreditCart()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
        }
        public AddCreditCart(RegisterModel model)
        {
            DataContext = _model = model;

            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            
            CancelButton.Click += CancelButton_Click;
        }
        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        private void ClearErrors()
        {
            TextBoxName.ErrorText = "";
            TextBoxNumber.ErrorText = "";
            TextBoxExpiryDate.ErrorText = "";
            TextBoxCVV.ErrorText = "";

        }
        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            string name = TextBoxName.Text;
            string number = TextBoxNumber.Text;
            string expiryDate = TextBoxExpiryDate.Text;
            string cvv = TextBoxCVV.Text;


            ClearErrors();
            bool hasErrors = false;

            if (_model.CreditCardName.IsNullOrEmpty())
            {
                TextBoxName.BringIntoView();
                TextBoxName.Focus();
                TextBoxName.ErrorText = "Name cannot be empty";
                hasErrors = true;
            }
            if (_model.CreditCardNumber.IsNullOrEmpty())
            {
                TextBoxNumber.BringIntoView();
                TextBoxNumber.Focus();
                TextBoxNumber.ErrorText = "Number cannot be empty";
                hasErrors = true;
            }
            if (_model.CreditCardExpiryDate.IsNullOrEmpty())
            {
                TextBoxExpiryDate.BringIntoView();
                TextBoxExpiryDate.Focus();
                TextBoxExpiryDate.ErrorText = "Expiry Date cannot be empty";
                hasErrors = true;
            }
            if (_model.CreditCardCVV.IsNullOrEmpty())
            {
                TextBoxCVV.BringIntoView();
                TextBoxCVV.Focus();
                TextBoxCVV.ErrorText = "CVV cannot be empty";
                hasErrors = true;
            }
            try
            {
                if (hasErrors) return;
                _model.ValidarCVV();
                _model.ValidarNumeroTarjeta();
                SmartTradeNavigationManager.Instance.MainView.HidePopUp();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Wrong number card. Only digits are allowed"))
                {
                    TextBoxNumber.ErrorText = ex.Message;
                }

                if (ex.Message.Contains("Wrong cvv. Only digits are allowed"))
                {
                    TextBoxCVV.ErrorText = ex.Message;
                }
                if (ex.Message.Contains("Incorrect format"))
                {
                    TextBoxExpiryDate.ErrorText = ex.Message;
                }
            }
        }
       // private void CancelButton_Click(object? sender, RoutedEventArgs e) => _ventana.Close();
        private void DateTextBox_TextInput(object sender, TextInputEventArgs e)
        {
            var textBox = (TextBox)sender;
            var text = textBox.Text;
            var caretIndex = textBox.CaretIndex;

            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
                return;
            }
            text = new string(text.Where(c => char.IsDigit(c)).ToArray());

            text = text.PadRight(4, '0');

            if (text.Length >= 2)
            {
                text = text.Insert(2, "/");
            }

            textBox.Text = text;

            textBox.CaretIndex = Math.Min(caretIndex + 1, text.Length);

            e.Handled = true;
        }
    }
}
