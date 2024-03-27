namespace SmartTradeLib.Entities;

public partial class Image
{
    public Image()
    {
    }

    public Image(byte[] image) : this()
    {
        ImageSource = image;
    }

    public override bool Equals(object? obj)
    {
        return obj is Image image && ImageSource.SequenceEqual(image.ImageSource);
    }

    public static bool operator ==(Image? image1, Image? image2)
    {
        return image1?.Equals(image2) ?? image2 is null;
    }

    public static bool operator !=(Image? image1, Image? image2)
    {
        return !(image1 == image2);
    }
}