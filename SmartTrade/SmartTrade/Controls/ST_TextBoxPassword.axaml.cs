using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace SmartTrade.Controls
{
    public partial class ST_TextBoxPassword : UserControl
    {
        public IRestrictor? Restrictor;

        public ST_TextBoxPassword()
        {
            InitializeComponent();

            InitializeComponent();
            SetRestrictors();

            ErrorText = "";
            ToggleButton.Click += (sender, obj) => { RevealPassword = (bool)ToggleButton.IsChecked; };
        }

        private void OnTextBoxOnTextChanged(object? sender, TextChangedEventArgs e)
        {
            Restrictor?.ApplyRestrictions();
        }

        public TextBox TextBox
        {
            get => MyTextBox;
            set => MyTextBox = value;
        }

        public TextBlock TextBlock
        {
            get => MyTextBlock;
            set => MyTextBlock = value;
        }

        public string? LabelText
        {
            get => GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public string Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string? WaterMark
        {
            get => MyTextBox.Watermark;
            set => MyTextBox.Watermark = value;
        }

        public double LabelWidth
        {
            get => MyTextBlock.Width;
            set => MyTextBlock.Width = value;
        }

        public double TextWidth
        {
            get => MyTextBox.Width;
            set => MyTextBox.Width = value;
        }

        public bool IsReadOnly
        {
            get => GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public double LabelHeight
        {
            get => MyTextBlock.Height;
            set => MyTextBlock.Height = value;
        }

        public double MaxLabelWidth
        {
            get => MyTextBlock.MaxWidth;
            set => MyTextBlock.MaxWidth = value;
        }

        public double TextHeight
        {
            get => MyTextBox.Height;
            set => MyTextBox.Height = value;
        }

        public double TextMinHeight
        {
            get => MyTextBox.MinHeight;
            set => MyTextBox.MinHeight = value;
        }

        public TextWrapping TextWrapping
        {
            get => MyTextBox.TextWrapping;
            set => MyTextBox.TextWrapping = value;
        }

        public bool AcceptsReturn
        {
            get => MyTextBox.AcceptsReturn;
            set => MyTextBox.AcceptsReturn = value;
        }

        public bool RevealPassword
        {
            get => MyTextBox.RevealPassword;
            set => MyTextBox.RevealPassword = value;
        }

        public string? PasswordText
        {
            get => GetValue(PasswordTextProperty);
            set => SetValue(PasswordTextProperty, value);
        }

        public string? ErrorText
        {
            get => ErrorMessage.Text;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    ErrorMessage.MaxHeight = 0;
                    ErrorMessage.Text = "";
                }
                else
                {
                    ErrorMessage.MaxHeight = double.PositiveInfinity;
                    ErrorMessage.Text = value;
                }
            }
        }

        public bool OnlyPositiveInt
        {
            get => GetValue(OnlyPositiveIntProperty);
            set => SetValue(OnlyPositiveIntProperty, value);
        }

        public bool OnlyLetters
        {
            get => GetValue(OnlyLettersProperty);
            set => SetValue(OnlyLettersProperty, value);
        }

        public bool OnlyPositiveDouble
        {
            get => GetValue(OnlyPositiveDoubleProperty);
            set => SetValue(OnlyPositiveDoubleProperty, value);
        }

        public string PatternRestriction
        {
            get => GetValue(PatternRestrictionProperty);
            set => SetValue(PatternRestrictionProperty, value);
        }

        private void SetRestrictors()
        {
            TextBox.TextChanged -= OnTextBoxOnTextChanged;
            TextBox.TextChanged += OnTextBoxOnTextChanged;

            if (OnlyPositiveInt)
            {
                Restrictor = new TextBoxRestrictorBuilder(MyTextBox).WithoutIntRestriction().WithPositiveRestriction().Build();
            }
            else if (OnlyPositiveDouble)
            {
                Restrictor = new TextBoxRestrictorBuilder(MyTextBox).WithoutDoubleRestriction().WithPositiveRestriction().Build();
            }
            else if (OnlyLetters)
            {
                Restrictor = new TextBoxRestrictorBuilder(MyTextBox).WithoutLetterRestriction().Build();
            }
            else if (!string.IsNullOrEmpty(PatternRestriction))
            {
                Restrictor = new TextBoxRestrictorBuilder(MyTextBox).WithoutPatterRestriction(PatternRestriction).Build();
            }
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            if (change.Property == OnlyPositiveDoubleProperty || change.Property == OnlyPositiveIntProperty || change.Property == PatternRestrictionProperty || change.Property == OnlyLettersProperty)
                SetRestrictors();
            base.OnPropertyChanged(change);
        }

        public static readonly StyledProperty<bool> OnlyPositiveIntProperty =
            AvaloniaProperty.Register<ST_TextBox, bool>(nameof(OnlyPositiveInt), defaultValue: false);

        public static readonly StyledProperty<bool> OnlyPositiveDoubleProperty =
            AvaloniaProperty.Register<ST_TextBox, bool>(nameof(OnlyPositiveDouble), defaultValue: false);

        public static readonly StyledProperty<string> PatternRestrictionProperty =
            AvaloniaProperty.Register<ST_TextBox, string>(nameof(PatternRestriction), defaultValue: "");

        public static readonly StyledProperty<bool> OnlyLettersProperty =
            AvaloniaProperty.Register<ST_TextBox, bool>(nameof(OnlyLetters), defaultValue: false);

        public static readonly StyledProperty<string?> TextProperty =
            AvaloniaProperty.Register<ST_TextBox, string?>(nameof(Text), defaultValue: "");

        public static readonly StyledProperty<string?> LabelTextProperty =
            AvaloniaProperty.Register<ST_TextBox, string?>(nameof(LabelText), defaultValue: "");

        public static readonly StyledProperty<bool> IsReadOnlyProperty =
            AvaloniaProperty.Register<ST_TextBox, bool>(nameof(IsReadOnly), defaultValue: false);

        public static readonly StyledProperty<string?> PasswordTextProperty =
            AvaloniaProperty.Register<ST_TextBox, string?>(nameof(PasswordText), defaultValue: "");
    }
}
