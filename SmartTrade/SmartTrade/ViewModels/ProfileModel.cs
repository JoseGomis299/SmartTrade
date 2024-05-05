using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartTrade.Services;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class ProfileModel : ViewModelBase
{
    public ObservableCollection<string> ProfileData { get; set; }
    public UserType LoggedType => Service.LoggedType;

    public bool IsParentalControlEnabled { get; set; } = true;
    public string Password { get; set; }

    public ProfileModel()
    {
        ProfileData = new ObservableCollection<string>();
        SetProfileData(Service.Logged);
    }

    private void OnToggleParentalControl()
    {
        throw new NotImplementedException();
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
}