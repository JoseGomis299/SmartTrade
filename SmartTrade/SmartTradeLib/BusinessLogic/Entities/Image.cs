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
}