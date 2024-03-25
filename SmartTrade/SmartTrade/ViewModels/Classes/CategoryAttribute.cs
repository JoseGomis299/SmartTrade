using System;
using ReactiveUI;

namespace SmartTrade.ViewModels;

public class CategoryAttribute : ReactiveObject
{
    public event Action OnValueChanged;
    private string? _value;
    private bool _isEnabled = true;

    public string? Name { get; set; }
    public string? Value
    {
        get => _value;
        set
        {
            this.RaiseAndSetIfChanged(ref _value, value);
            OnValueChanged?.Invoke();
        }
    }
    public bool OnlyInt { get; set; }
    public bool OnlyFloat { get; set; }
    public bool IsEnabled 
    {
        get => _isEnabled;
        set => this.RaiseAndSetIfChanged(ref _isEnabled, value);
    }

    public CategoryAttribute(string name)
    {
        if (name.EndsWith('i')) OnlyInt = true;
        else if (name.EndsWith('f')) OnlyFloat = true;

        if (name[name.Length - 2] == '/')
            Name = name.Substring(0, name.Length - 2);
        else Name = name;
    }

    public override bool Equals(object? obj)
    {
        return obj is CategoryAttribute attribute && attribute.Name == Name && attribute.Value == Value;
    }
}