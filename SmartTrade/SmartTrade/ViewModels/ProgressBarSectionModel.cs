using Avalonia.Media;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace SmartTrade.ViewModels;

public class ProgressBarSectionModel : ViewModelBase
{
    public string? Title { get; set; }
    public Bitmap? Image { get; set; }

    public bool? IsLast { get; set; }
    public SolidColorBrush TextColor { get; private set; }

    public ProgressBarSectionModel()
    {
    }

    public ProgressBarSectionModel(string? title, bool? isLast, Bitmap? image)
    {
        Title = title;
        IsLast = isLast;
        Image = image;
        TextColor = new SolidColorBrush(Color.FromRgb(10, 41, 62));
    }

    public void SelectSection()
    {
        TextColor.Color = Color.FromRgb(32,195,78);
        this.RaisePropertyChanged(nameof(TextColor));
        this.RaisePropertyChanged(nameof(Image));
    }

    public void DeselectSection()
    {
        TextColor.Color = Color.FromRgb(10, 41, 62);
        this.RaisePropertyChanged(nameof(TextColor));
        this.RaisePropertyChanged(nameof(Image));
    }

}
