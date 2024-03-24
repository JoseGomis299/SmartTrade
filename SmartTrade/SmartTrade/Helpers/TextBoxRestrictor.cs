using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace SmartTrade.Restrictors
{
    public class TextBoxRestrictor : IRestrictor
    {
        public bool AllowNegative { get; set; }
        private readonly List<Func<bool>> _restrictions;
        private readonly TextBox _textBox;

        public string? Text
        {
            get => _textBox.Text;
            set => _textBox.Text = value;
        }
        public int CaretIndex
        {
            get => _textBox.CaretIndex;
            set => _textBox.CaretIndex = value;
        }

        public TextBoxRestrictor(TextBox textBox)
        {
            _restrictions = new List<Func<bool>>();
            AllowNegative = true;
            _textBox = textBox;
        }

        public void AddRestriction(Func<bool> restriction)
        {
            _restrictions.Add(restriction);
        }

        public void ApplyRestrictions()
        {
            if(string.IsNullOrEmpty(Text)) return;
            bool removeLastChar = true;

            foreach (var restriction in _restrictions)
            {
                if (!restriction())
                {
                    removeLastChar = false;
                    break;
                }
            }

            if (removeLastChar)
            {
                Text = Text.Remove(Text.Length - 1);
            }
        }
    }
}
