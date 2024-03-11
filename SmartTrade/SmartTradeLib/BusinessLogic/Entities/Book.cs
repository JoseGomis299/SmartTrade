using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartTradeLib.Entities;

public partial class Book : Product
{
    public Book(string name, string certification, string ecologicPrint, int minimumAge, bool validated, byte[] image, Post post, string author, string publisher, string pages, string language, string isbn) : base(name, certification, ecologicPrint, minimumAge, validated, image, post)
    {
        Author = author;
        Publisher = publisher;
        Pages = pages;
        Language = language;
        ISBN = isbn;
    }

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