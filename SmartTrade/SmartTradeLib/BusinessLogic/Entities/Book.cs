namespace SmartTradeLib.Entities;

public partial class Book : Product
{
    public override string GetInfo()
    {
        return
            $"- Author: {Author}" +
            $"\n- Publisher: {Publisher}" +
            $"\n- Number of pages: {Pages}" +
            $"\n- Language: {Language}" +
            $"\n- ISBN: {ISBN}";
    }

    public override ICollection<Category> GetCategories()
    {
        return new List<Category> { Category.Book };
    }

    public override string GetDifferences(Product product)
    {
        string differences = "";
     
        if (product is Book book)
        {
            if (Language != book.Language)
            {
                differences += Language;
            }
        }

        return differences;
    }
}