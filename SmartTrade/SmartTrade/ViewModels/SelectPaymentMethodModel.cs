using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using SmartTrade.Entities;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class SelectPaymentMethodModel : ViewModelBase
{

    public ObservableCollection<PaymentMethodModel> CreditCards { get; set; }
    public ObservableCollection<PaymentMethodModel> Paypals { get; set; }
    public ObservableCollection<PaymentMethodModel> Bizums { get; set; }
    private List<BizumInfo> _tempBizums;
    private List<CreditCardInfo> _tempCreditCards;
    private List<PayPalInfo> _tempPaypals;

    public PaymentMethodModel? SelectedCreditCard
    {
        get => _selectedCreditCard;
        set
        {
            _selectedCreditCard = value;
            this.RaisePropertyChanged(nameof(SelectedCreditCard));
        }
    }

    public PaymentMethodModel? SelectedPaypal
    {
        get => _selectedPaypal;
        set
        {
            _selectedPaypal = value;
            this.RaisePropertyChanged(nameof(SelectedPaypal));
        }
    }

    public PaymentMethodModel? SelectedBizum
    {
        get => _selectedBizum;
        set
        {
            _selectedBizum = value;
            this.RaisePropertyChanged(nameof(SelectedBizum));
        }
    }

    public PaymentMethodModel SelectedPaymentMethod
    {
        get
        {
            if (SelectedCreditCard != null)
            {
                return SelectedCreditCard;
            }
            else if (SelectedPaypal != null)
            {
                return SelectedPaypal;
            }
            else
            {
                return SelectedBizum;
            }
        }
    }

    private PaymentMethodModel? _selectedCreditCard;
    private PaymentMethodModel? _selectedPaypal;
    private PaymentMethodModel? _selectedBizum;

    public SelectPaymentMethodModel()
    {
        CreditCards = new ObservableCollection<PaymentMethodModel>();
        Paypals = new ObservableCollection<PaymentMethodModel>();
        Bizums = new ObservableCollection<PaymentMethodModel>();
        
        _tempBizums = new List<BizumInfo>();
        _tempCreditCards = new List<CreditCardInfo>();
        _tempPaypals = new List<PayPalInfo>();

        if (Service.Logged != null)
        {
            UpdatePaymentMethods();
        }
    }

    private void UpdatePaymentMethods()
    {
        CreditCards.Clear();
        Paypals.Clear();
        Bizums.Clear();

        foreach (var creditCard in (Service.Logged as ConsumerDTO).CreditCards)
        {
            CreditCards.Add(new PaymentMethodModel(creditCard, this));
        }

        foreach (var paypal in (Service.Logged as ConsumerDTO).PayPalAccounts)
        {
            Paypals.Add(new PaymentMethodModel(paypal, this));
        }

        foreach (var bizum in (Service.Logged as ConsumerDTO).BizumAccounts)
        {
            Bizums.Add(new PaymentMethodModel(bizum, this));
        }

        foreach (var bizum in _tempBizums)
        {
            Bizums.Add(new PaymentMethodModel(bizum, this));
        }

        foreach (var creditCard in _tempCreditCards)
        {
            CreditCards.Add(new PaymentMethodModel(creditCard, this));
        }

        foreach (var paypal in _tempPaypals)
        {
            Paypals.Add(new PaymentMethodModel(paypal, this));
        }

        this.RaisePropertyChanged(nameof(CreditCards));
        this.RaisePropertyChanged(nameof(Paypals));
        this.RaisePropertyChanged(nameof(Bizums));
    }

    public async Task AddBizumAsync(BizumInfo bizum, bool? save)
    {
        if (save == true)
        {
            await Service.AddBizumAsync(bizum);
        }
        else _tempBizums.Add(bizum);

        UpdatePaymentMethods();
    }

    public async Task AddCreditCardAsync(CreditCardInfo creditCard, bool? save)
    {
        if (save == true)
        {
            await Service.AddCreditCardAsync(creditCard);
        }
        else _tempCreditCards.Add(creditCard);

        UpdatePaymentMethods();
    }

    public async Task AddPaypalAsync(PayPalInfo paypal, bool? save)
    {
        if (save == true)
        {
            await Service.AddPaypalAsync(paypal);
        }
        else _tempPaypals.Add(paypal);

        UpdatePaymentMethods();
    }
}

public class PaymentMethodModel : ReactiveObject
{
    public string Name { get; private set; }
    public string Number { get; private set; }
    public string Type { get; private set; }

    private bool _isChecked;

    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            _isChecked = value;
            this.RaisePropertyChanged(nameof(IsChecked));
        }
    }

    public ICommand ChangePaymentMethodCommand { get; set; }

    public PaymentMethodModel(CreditCardInfo creditCard)
    {
        Name = creditCard.CardHolder;
        Number = creditCard.CardNumber;
        Type = "Credit Card";
    }

    public PaymentMethodModel(CreditCardInfo creditCard, SelectPaymentMethodModel selectPaymentMethodModel)
    {
        Name = creditCard.CardHolder;
        Number = creditCard.CardNumber;
        Type = "Credit Card";

        ChangePaymentMethodCommand = ReactiveCommand.Create(() =>
        {
            ResetAllToggles(selectPaymentMethodModel);

            IsChecked = true;
            selectPaymentMethodModel.SelectedCreditCard = this;
        });
    }

    public PaymentMethodModel(PayPalInfo paypal)
    {
        Name = paypal.Email;
        Number = "";
        Type = "Paypal";
    }

    public PaymentMethodModel(PayPalInfo paypal, SelectPaymentMethodModel selectPaymentMethodModel)
    {
        Name = paypal.Email;
        Number = "";
        Type = "Paypal";

        ChangePaymentMethodCommand = ReactiveCommand.Create(() =>
        {
            ResetAllToggles(selectPaymentMethodModel);

            IsChecked = true;
            selectPaymentMethodModel.SelectedPaypal = this;
        });
    }

    public PaymentMethodModel(BizumInfo bizum)
    {
        Name = "";
        Number = bizum.TelephonNumber;
        Type = "Bizum";
    }

    public PaymentMethodModel(BizumInfo bizum, SelectPaymentMethodModel selectPaymentMethodModel)
    {
        Name = "";
        Number = bizum.TelephonNumber;
        Type = "Bizum";

        ChangePaymentMethodCommand = ReactiveCommand.Create(() =>
        {
            ResetAllToggles(selectPaymentMethodModel);

            IsChecked = true;
            selectPaymentMethodModel.SelectedBizum = this;
        });
    }

    private void ResetAllToggles(SelectPaymentMethodModel selectPaymentMethodModel)
    {
        foreach (var paymentMethod in selectPaymentMethodModel.CreditCards)
        {
            paymentMethod.IsChecked = false;
        }

        foreach (var paymentMethod in selectPaymentMethodModel.Paypals)
        {
            paymentMethod.IsChecked = false;
        }

        foreach (var paymentMethod in selectPaymentMethodModel.Bizums)
        {
            paymentMethod.IsChecked = false;
        }

        selectPaymentMethodModel.SelectedCreditCard = null;
        selectPaymentMethodModel.SelectedPaypal = null;
        selectPaymentMethodModel.SelectedBizum = null;
    }
}
