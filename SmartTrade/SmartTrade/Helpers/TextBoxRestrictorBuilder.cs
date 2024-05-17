using System.Globalization;
using Avalonia.Controls;
using SmartTrade.Restrictors;

namespace SmartTrade;
public class TextBoxRestrictorBuilder
{
    private TextBoxRestrictor _textBoxRestrictor;

    public TextBoxRestrictorBuilder(TextBox textBox)
    {
        _textBoxRestrictor = new TextBoxRestrictor(textBox);
    }

    public TextBoxRestrictorBuilder WithoutIntRestriction()
    {
        _textBoxRestrictor.AddRestriction(() =>
        {
            if (_textBoxRestrictor.Text.Length == 1 && _textBoxRestrictor.Text[0] == '-')
            {
                return !_textBoxRestrictor.AllowNegative;
            }

            if (_textBoxRestrictor.Text.Length > 0 && int.TryParse(_textBoxRestrictor.Text, out _))
            {
                return false;
            }

            if (_textBoxRestrictor.Text.Length == 0)
            {
                _textBoxRestrictor.Text = "0";
                _textBoxRestrictor.CaretIndex = _textBoxRestrictor.Text.Length;
                return false;
            }

            return true;
        });

        return this;
    }

    public TextBoxRestrictorBuilder WithoutDoubleRestriction()
    {
        _textBoxRestrictor.AddRestriction(() =>
        {
            char decimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0];
            char notDecimalSeparator = decimalSeparator == '.' ? ',' : '.';

            if (_textBoxRestrictor.Text.Length == 1 && _textBoxRestrictor.Text[0] == '-')
            {
                return !_textBoxRestrictor.AllowNegative;
            }

            if (_textBoxRestrictor.Text.Length > 1 && _textBoxRestrictor.Text[^1] == notDecimalSeparator)
            {
                _textBoxRestrictor.Text = _textBoxRestrictor.Text.Remove(_textBoxRestrictor.Text.Length - 1);
                _textBoxRestrictor.Text += decimalSeparator;
                _textBoxRestrictor.CaretIndex = _textBoxRestrictor.Text.Length;
                return false;
            }

            if (_textBoxRestrictor.Text.Length > 0 && double.TryParse(_textBoxRestrictor.Text, out _))
            {
                return false;
            }

            if (_textBoxRestrictor.Text.Length == 1 && (_textBoxRestrictor.Text[0] == decimalSeparator || _textBoxRestrictor.Text[0] == notDecimalSeparator))
            {
                _textBoxRestrictor.Text = "0" + decimalSeparator;
                _textBoxRestrictor.CaretIndex = _textBoxRestrictor.Text.Length;
                return false;
            }

            if (_textBoxRestrictor.Text.Length == 0)
            {
                _textBoxRestrictor.Text = "0";
                _textBoxRestrictor.CaretIndex = _textBoxRestrictor.Text.Length;
                return false;
            }

            return true;
        });

        return this;
    }

    public TextBoxRestrictorBuilder WithPositiveRestriction()
    {
        _textBoxRestrictor.AllowNegative = false;
        return this;
    }

    public TextBoxRestrictorBuilder WithLengthRestriction(int length)
    {
       _textBoxRestrictor.MaxLength = length;
        return this;
    }

    public TextBoxRestrictorBuilder WithoutLetterRestriction()
    {
        _textBoxRestrictor.AddRestriction(() =>
        {
            if (_textBoxRestrictor.Text.Length > 0 && char.IsLetter(_textBoxRestrictor.Text[^1]))
            {
                return false;
            }

            return true;
        });

        return this;
    }

    public TextBoxRestrictorBuilder WithoutSymbolRestriction()
    {
        _textBoxRestrictor.AddRestriction(() =>
        {
            string pattern = "[!\"·${}%&/(<)\\ºª=¿¡?'_:;,|@#€*+-`^´´¨. ]";

            if (_textBoxRestrictor.Text.Length > 0 && pattern.Contains(_textBoxRestrictor.Text[^1]))
            {
                return false;
            }

            return true;
        });

        return this;
    }

    public TextBoxRestrictorBuilder WithoutPatterRestriction(string pattern)
    {
        _textBoxRestrictor.AddRestriction(() =>
        {
            if (_textBoxRestrictor.Text.Length > 0 && pattern.Contains(_textBoxRestrictor.Text[^1]))
            {
                return false;
            }

            return true;
        });

        return this;
    }

    public TextBoxRestrictorBuilder WithoutRestrictions()
    {
        _textBoxRestrictor.AddRestriction(() =>
        {
            return false;
        });

        return this;
    }

    public TextBoxRestrictor Build()
    {
        return _textBoxRestrictor;
    }
}