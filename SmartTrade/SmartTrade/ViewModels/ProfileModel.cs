using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SmartTrade.Services;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class ProfileModel : ViewModelBase
{
    public ObservableCollection<string> ProfileData { get; set; }
    public UserType LoggedType => Service.LoggedType;

    public UserDTO User
    {
        get
        {
            return Service.Logged;
        }
    }

    public bool IsParentalControlEnabled { get; set; }
    public string Password { get; set; }

    public ProfileModel()
    {
        ProfileData = new ObservableCollection<string>();
        SetProfileData(Service.Logged);
    }

    public DateTime getBirth(UserDTO? user)
    {
        try { 
            ConsumerDTO consumer = (ConsumerDTO)user;
            return consumer.BirthDate;
        }
        catch { throw new Exception("Not Consumer");}
        
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
            return true;
        }
        else
        {
            return false;
        }
    }
}