using Avalonia.Controls;
using Avalonia.Metadata;
using SmartTrade.Restrictors;

namespace SmartTrade.Controls
{
    public partial class ST_TextBox : UserControl
    {
        public IRestrictor? Restrictor;

        public ST_TextBox()
        {
            InitializeComponent();
            TextBox.TextChanged += (sender, e) => Restrictor?.ApplyRestrictions();
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
            get => MyTextBlock.Text;
            set => MyTextBlock.Text = value;
        }

        public string? Text
        {
            get => MyTextBox.Text;
            set => MyTextBox.Text = value;
        }

        public string? WaterMark
        {
            get => MyTextBox.Watermark;
            set => MyTextBox.Watermark = value;
        }

        public double TextWidth
        {
            get => MyTextBox.Width;
            set => MyTextBox.Width = value;
        }

        public double LableWidth
        {
            get => MyTextBlock.Width;
            set => MyTextBlock.Width = value;
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

        public bool AcceptsReturn
        {
            get => MyTextBox.AcceptsReturn;
            set => MyTextBox.AcceptsReturn = value;
        }

        public bool OnlyPositiveInt
        {
            set
            {
                if (value)
                    Restrictor = new TextBoxRestrictorBuilder(MyTextBox).WithoutIntRestriction().WithPositiveRestriction().Build();
            }
        }

        public bool OnlyPositiveDouble
        {
            set
            {
                if (value)
                    Restrictor = new TextBoxRestrictorBuilder(MyTextBox).WithoutDoubleRestriction().WithPositiveRestriction().Build();
            }
        }
    }
}
