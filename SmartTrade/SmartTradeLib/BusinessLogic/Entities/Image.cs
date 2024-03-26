namespace SmartTradeLib.Entities;

public partial class Image
{
    public Image()
    {
        Products = new List<Product>();
    }

    public Image(byte[] image) : this()
    {
        ImageSource = image;
    }
}