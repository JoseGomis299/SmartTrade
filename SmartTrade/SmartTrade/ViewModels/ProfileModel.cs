using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI;
using SmartTrade.Entities;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class ProfileModel : ViewModelBase
{
    public ObservableCollection<PaymentMethodModel> CreditCards { get; set; } = new ObservableCollection<PaymentMethodModel>();
    public ObservableCollection<PaymentMethodModel> Paypals { get; set; } = new ObservableCollection<PaymentMethodModel>();
    public ObservableCollection<PaymentMethodModel> Bizums { get; set; } = new ObservableCollection<PaymentMethodModel>();
    public ObservableCollection<AddressModel> Addresses { get; set; } = new ObservableCollection<AddressModel>();

    public ObservableCollection<string> ProfileData { get; set; }
    public UserType LoggedType => Service.LoggedType;

    public UserDTO User => Service.Logged;

    public bool IsParentalControlEnabled { get; set; }
    public string Password { get; set; }

    public ProfileModel()
    {
        ProfileData = new ObservableCollection<string>();
        SetProfileData(Service.Logged);
        IsParentalControlEnabled = Service.IsParentalControlEnabled;

        UpdatePaymentMethods();
        UpdateAddresses();
    }
    public void UpdateParentalControlStatus()
    {
        Service.SetIsParentalControlEnabled(IsParentalControlEnabled);
        SmartTradeNavigationManager.Instance.MainView.ReinitializeHomeNextTime = true;
    }

    private void UpdatePaymentMethods()
    {
        CreditCards.Clear();
        Paypals.Clear();
        Bizums.Clear();

        foreach (var creditCard in (Service.Logged as ConsumerDTO).CreditCards)
        {
            CreditCards.Add(new PaymentMethodModel(creditCard));
        }

        foreach (var paypal in (Service.Logged as ConsumerDTO).PayPalAccounts)
        {
            Paypals.Add(new PaymentMethodModel(paypal));
        }

        foreach (var bizum in (Service.Logged as ConsumerDTO).BizumAccounts)
        {
            Bizums.Add(new PaymentMethodModel(bizum));
        }

        this.RaisePropertyChanged(nameof(CreditCards));
        this.RaisePropertyChanged(nameof(Paypals));
        this.RaisePropertyChanged(nameof(Bizums));
    }

    public async Task AddBizumAsync(BizumInfo bizum)
    {
        await Service.AddBizumAsync(bizum);
        UpdatePaymentMethods();
    }

    public async Task AddCreditCardAsync(CreditCardInfo creditCard)
    {
        await Service.AddCreditCardAsync(creditCard);
        UpdatePaymentMethods();
    }

    public async Task AddPaypalAsync(PayPalInfo paypal)
    {
        await Service.AddPaypalAsync(paypal);
        UpdatePaymentMethods();
    }

    private void UpdateAddresses()
    {
        Addresses.Clear();

        foreach (var address in (Service.Logged as ConsumerDTO).Addresses)
        {
            Addresses.Add(new AddressModel(address));
        }
    }

    public async Task AddAddressAsync(Address address)
    {
        await Service.AddAddressAsync(address);
        UpdateAddresses();
    }

    public void SetProfileData(UserDTO? user)
    {
        ProfileData.Clear();

        if (user == null)
        {
            return;
        }

        ProfileData.Add($"Full Name: {user.Name + " " + user.LastNames}");
        ProfileData.Add($"Email: {user.Email}");

        if (user.IsSeller)
        {
            SellerDTO seller = (SellerDTO)user;

            ProfileData.Add($"Company Name: {seller.CompanyName}");
            ProfileData.Add($"Posts Count: {seller.PostIds.Count}");
            ProfileData.Add("User Type: Seller");
        }
        else if (user.IsConsumer)
        {
            ConsumerDTO consumer = (ConsumerDTO)user;

            ProfileData.Add($"Birth Date: {consumer.BirthDate.ToShortDateString()}");
            ProfileData.Add("User Type: Consumer");
        }
        else if (user.IsAdmin)
        {
            ProfileData.Add("User Type: Admin");
        }
    }
    public async Task LogOut()
    {
        await Service.LogOut();
    }

    public bool IsCorrectPassword()
    {
        if (Password == Service.Logged.Password)
        {
            return true;
        }
        else { return false; }
    }
    public bool ParentalControlerChecker(DateTime BirthDate)
    {
        DateTime currentDate = DateTime.Now;
        int totalDays = currentDate.Day - BirthDate.Day;
        int totalMonths = currentDate.Month - BirthDate.Month;
        int totalYears = currentDate.Year - BirthDate.Year;
        if (totalDays < 0)
        {
            totalDays += DateTime.DaysInMonth(BirthDate.Year, BirthDate.Month);
            totalMonths--;
        }
        if (totalMonths < 0)
        {
            totalMonths += 12;
            totalYears--;
        }
        int age = totalYears;
        if (totalMonths > 0 || totalDays > 0)
        {
            age++;
        }
        if (age >= 18)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}