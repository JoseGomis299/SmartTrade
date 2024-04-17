﻿using System.Collections.ObjectModel;
using ReactiveUI;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class ProfileModel : ReactiveObject
{
    public ObservableCollection<string> ProfileData { get; set; }

    public ProfileModel()
    {
        ProfileData = new ObservableCollection<string>();
        SetProfileData(SmartTradeService.Instance.Logged);
    }

    public void SetProfileData(UserDTO? user)
    {
        ProfileData.Clear();

        if (user == null)
        {
            return;
        }

        ProfileData.Add($"Full Name: {user.Name + " " +user.LastNames}");
        ProfileData.Add($"Email: {user.Email}");

        if (user.IsSeller)
        {
            SellerDTO seller = (SellerDTO) user;

            ProfileData.Add($"Company Name: {seller.CompanyName}");
            ProfileData.Add($"Posts Count: {seller.PostIds.Count}");
            ProfileData.Add("User Type: Seller");
        }
        else if (user.IsConsumer)
        {
            ConsumerDTO consumer = (ConsumerDTO) user;

            ProfileData.Add($"Birth Date: {consumer.BirthDate.ToShortDateString()}");
            ProfileData.Add("User Type: Consumer");
        }
        else if (user.IsAdmin)
        {
            ProfileData.Add("User Type: Admin");
        }
    }
}