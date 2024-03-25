using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace SmartTrade.ViewModels;

public class ImageSource
{
    public Bitmap? Image { get; set; }

    public byte[] Bytes { get; set; }

    public ICommand RemoveImage { get; }

    public ImageSource(byte[] image, Stock stock)
    {
        Bytes = image;
        Image = image.ToBitmap();

        RemoveImage = ReactiveCommand.Create(() =>
        {
            stock.Images.Remove(this);
        });
    }
}