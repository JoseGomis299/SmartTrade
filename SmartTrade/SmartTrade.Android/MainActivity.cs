﻿using System.Threading;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Avalonia;
using Avalonia.Android;
using Avalonia.Platform;
using Avalonia.ReactiveUI;
using Java.Security;

namespace SmartTrade.Android
{

    [Activity(
        Label = "SmartTrade.Android",
        Theme = "@style/MyTheme.NoActionBar",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
    public class MainActivity : AvaloniaMainActivity<App>
    {
        protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
        {
            return base.CustomizeAppBuilder(builder)
                .WithInterFont()
                .UseReactiveUI();
        }

        public override void OnBackPressed()
        {
            if (SmartTradeNavigationManager.Instance.MainView.ShowingPopUp)
            {
                SmartTradeNavigationManager.Instance.MainView.HidePopUp();
                return;
            }

            if (!SmartTradeNavigationManager.Instance.NavigateBack())
            {
                base.OnBackPressed();
            }
        }
    }
}

