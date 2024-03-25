namespace SmartTradeLib.Entities;

public partial class Image
{
    public Image()
    {
        Products = new HashSet<Product>();
    }

    public Image(byte[] image) : this()
    {
        ImageSource = image;
    }
}