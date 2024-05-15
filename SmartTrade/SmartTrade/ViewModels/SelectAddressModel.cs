using System.Collections.ObjectModel;
using Avalonia.Controls;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartTrade.Entities;
using SmartTradeDTOs;
using static System.Net.Mime.MediaTypeNames;
using static Android.Graphics.ColorSpace;
using Android.Locations;
using Address = SmartTrade.Entities.Address;

namespace SmartTrade.ViewModels;

public class SelectAddressModel : ViewModelBase
{
    public ObservableCollection<AddressModel> Addresses { get; set; }
    public ObservableCollection<AddressModel> BillingAddresses { get; set; }

    public Address SelectedAddress
    {
        get => _selectedAddress;
        set
        {
            _selectedAddress = value;
            this.RaisePropertyChanged(nameof(SelectedAddress));
        }
    }

    public Address SelectedBillingAddress
    {
        get => _selectedBillingAddress;
        set
        {
            _selectedBillingAddress = value;
            this.RaisePropertyChanged(nameof(SelectedBillingAddress));
        }
    }

    private Address _selectedBillingAddress;
    private Address _selectedAddress;

    public SelectAddressModel()
    {
        Addresses = new ObservableCollection<AddressModel>();
        BillingAddresses = new ObservableCollection<AddressModel>();

        if (Service.Logged != null)
        {
            UpdateAddresses();

            Addresses[0].SetChecked(true);
            BillingAddresses[0].SetChecked(true);
            SelectedAddress = (Service.Logged as ConsumerDTO).Addresses[0];
            SelectedBillingAddress = (Service.Logged as ConsumerDTO).Addresses[0];
        }
    }

    private void UpdateAddresses()
    {
        Addresses.Clear();
        BillingAddresses.Clear();

        foreach (var address in (Service.Logged as ConsumerDTO).Addresses)
        {
            Addresses.Add(new AddressModel(address, this, false));
            BillingAddresses.Add(new AddressModel(address, this, true));
        }
    }

    public async Task AddAddressAsync(Address address, bool? save)
    {
        if (save == true)
            await Service.AddAddressAsync(address);
        else 
            Service.AddBillingAddressLocal(address);
        UpdateAddresses();
    }
}

public class AddressModel : ViewModelBase
{
    public string? Dir1 { get; set; }
    public string? Dir2 { get; set; }
    public string? Dir3 { get; set; }

    public ICommand ChangeAddressCommand { get; set; }
    public bool IsChecked { get; set; }


    public AddressModel(Address address, SelectAddressModel model, bool isBillingAddress)
    {
        Dir1 = address.Street + " " + address.Number;
        Dir2 = "Door " + address.Door;
        Dir3 = $"{address.Province}, {address.City} {address.PostalCode}";

        ChangeAddressCommand = ReactiveCommand.Create(()=>SelectAddress(address, model, isBillingAddress));
    }

    private void SelectAddress(Address address, SelectAddressModel model, bool isBillingAddress)
    {
        foreach (var item in isBillingAddress ? model.BillingAddresses : model.Addresses)
        {
            item.SetChecked(false);
        }

        SetChecked(true);

        if (isBillingAddress)
        {
            model.SelectedBillingAddress = address;
        }
        else
        {
            model.SelectedAddress = address;
        }
    }


    public void SetChecked(bool value)
    {
        IsChecked = value;
        this.RaisePropertyChanged(nameof(IsChecked));
    }

}