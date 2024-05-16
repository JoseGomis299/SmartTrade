using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SmartTrade.ViewModels;

namespace SmartTrade.Controls
{
    public partial class ST_ProgessBar : UserControl
    {
        private ObservableCollection<ProgressBarSectionModel> _sections { get; } = new();

        private int _lineLength { get; set; }
        private int _currentSection;
        
        private Bitmap? selectedImage;
        private Bitmap? unselectedImage;

        public ST_ProgessBar()
        {
            selectedImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/CheckboxSelected.png")));
            unselectedImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/CheckboxUnselected.png")));
            _currentSection = -1;

            InitializeComponent();
        }
        public string? Sections
        {
            get => GetValue(SectionsProperty);
            set => SetValue(SectionsProperty, value);
        }

        public int SelectedSection
        {
            get => GetValue(SelectedSectionProperty);
            set => SetValue(SelectedSectionProperty, value);
        }

        public static readonly StyledProperty<string?> SectionsProperty =
            AvaloniaProperty.Register<ST_ProgessBar, string?>(nameof(Sections));

        public static readonly StyledProperty<int> SelectedSectionProperty =
            AvaloniaProperty.Register<ST_ProgessBar, int>(nameof(SelectedSection));

        public void NextSection()
        {
            if (_currentSection < _sections.Count - 1)
            {
                SelectIndex(++_currentSection);
            }
        }

        public void PreviousSection()
        {
            if (_currentSection > 0)
            {
                SelectIndex(--_currentSection);
            }
        }

        public void SelectIndex(int index)
        {
            if (_currentSection != -1)
            {
                _sections[_currentSection].Image = unselectedImage;
                _sections[_currentSection].DeselectSection();
            }

            _currentSection = index;
            _sections[index].Image = selectedImage;
            _sections[index].SelectSection();
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == SectionsProperty)
            {
                _sections.Clear();
                if (change.NewValue is string sections)
                {
                    string[] sectionNames = sections.Split(',');
                    _sections.Add(new ProgressBarSectionModel(sectionNames[0].Trim(), false, selectedImage));
                    _sections[0].SelectSection();
                    _currentSection = 0;

                    for (var i = 1; i < sectionNames.Length-1; i++)
                    {
                        _sections.Add(new ProgressBarSectionModel(sectionNames[i].Trim(), false, unselectedImage));
                    }
                    _sections.Add(new ProgressBarSectionModel(sectionNames[^1].Trim(), true, unselectedImage));

                    _lineLength = (_sections.Count - 1) * 85;
                }
            }
            else if (change.Property == SelectedSectionProperty)
            {
                SelectIndex(SelectedSection);
            }
        }
    }
}
