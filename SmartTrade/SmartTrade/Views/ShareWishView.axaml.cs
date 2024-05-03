using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using System;
using System.Net.Mail;
using System.Net;
using SmartTrade.Services;

namespace SmartTrade.Views
{
    public partial class ShareWishView : UserControl
    {
        private Popup _popup;
        public ShareWishView()
        {
            InitializeComponent();

            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
            _popup = new Popup
            {
                Child = this,
                IsLightDismissEnabled = true
            };
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            SendEmail(TextBoxEmail.Text);
        }

        public void SendEmail(string email)
        {
            // Dirección de correo electrónico del remitente
            string fromAddress = "smart.trade.app.24@gmail.com";

            // Configuración del servidor SMTP
            string smtpHost = "smtp.gmail.com"; // Servidor SMTP de Gmail
            int smtpPort = 587; // Puerto SMTP para Gmail
            string smtpUsername = "smart.trade.app.24@gmail.com"; // Tu dirección de correo electrónico
            string smtpPassword = "SmartTradeMolaMazo"; // Tu contraseña

            // Crear el mensaje de correo electrónico
            MailMessage mailMessage = new MailMessage(fromAddress, email);
            mailMessage.Subject = "Mira mi WishList";
            mailMessage.Body = SmartTradeService.Instance.Logged.Name + " te ha enviado su WishList.";

            // Configurar el cliente SMTP
            SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
            smtpClient.EnableSsl = true; // Habilitar SSL
            smtpClient.UseDefaultCredentials = false; // No usar credenciales predeterminadas
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

            try
            {
                // Enviar el correo electrónico
                smtpClient.Send(mailMessage);
                Console.WriteLine("Correo electrónico enviado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
            }
            finally
            {
                // Liberar recursos
                mailMessage.Dispose();
                smtpClient.Dispose();
            }
        }
    }
}
