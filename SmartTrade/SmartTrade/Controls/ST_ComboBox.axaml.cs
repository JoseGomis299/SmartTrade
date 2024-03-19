using Avalonia.Controls;

namespace SmartTrade.Controls
{
    public partial class ST_ComboBox : UserControl
    {
        public ST_ComboBox()
        {
            InitializeComponent();
        }

        public ComboBox ComboBox
        {
            get => MyComboBox;
            set => MyComboBox = value;
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

        public string? SelectedItem
        {
            get => MyComboBox.SelectedItem?.ToString();
            set => MyComboBox.SelectedItem = value;
        }

        public string? PlaceHolderText
        {
            get => MyComboBox.PlaceholderText; 
            set => MyComboBox.PlaceholderText = value;
        }
    }
}
