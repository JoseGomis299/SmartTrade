using Avalonia;
using Avalonia.Controls;

namespace SmartTrade.Controls;

public class RestrictedTextBox : TextBox
{
    public IRestrictor Restrictor { get; set; }

    public RestrictedTextBox()
    {
        TextChanged += (sender, e) => Restrictor.ApplyRestrictions();
    }

    public bool OnlyPositiveInt
    {
        set
        {
            if (value)
            {
                Restrictor = new TextBoxRestrictorBuilder(this).WithoutIntRestriction().WithPositiveRestriction().Build();
            }
        }
    }

    public bool OnlyPositiveDouble
    {
        set
        {
            if (value)
            {
                Restrictor = new TextBoxRestrictorBuilder(this).WithoutDoubleRestriction().WithPositiveRestriction().Build();
            }
        }
    }

    public static readonly StyledProperty<bool> OnlyPositiveIntProperty =
        AvaloniaProperty.Register<ST_TextBox, bool>(nameof(OnlyPositiveInt), defaultValue: false);


    public static readonly StyledProperty<bool> OnlyPositiveDoubleProperty =
        AvaloniaProperty.Register<ST_TextBox, bool>(nameof(OnlyPositiveDouble), defaultValue: false);
}