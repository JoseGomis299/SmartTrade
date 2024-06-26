﻿using SmartTrade.BusinessLogic;

namespace SmartTrade.Entities;

public partial class Clothing : Product
{
    public Clothing() { }

    //public Clothing(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string brand, string size, string color, string material) : base(name, certification, ecologicPrint, minimumAge, howToUse, howToReducePrint)
    //{
    //    Brand = brand;
    //    Size = size;
    //    Color = color;
    //    Material = material;
    //}

    public override string GetInfo()
    {
        return $"- Brand: {Brand}" +
               $"\n- Size: {Size}" +
               $"\n- Color: {Color}" +
               $"\n- Material: {Material}";
    }

    public override Dictionary<string, string> GetAttributes()
    {
        return new Dictionary<string, string>
        {
            { nameof(Brand), Brand },
            { nameof(Size), Size },
            { nameof(Color), Color },
            { nameof(Material), Material }
        };
    }

    public override ICollection<Category> GetCategories()
    {
        return new List<Category> { Category.Clothing };
    }

    public override string GetDifferences(Product product)
    {
       string differences = "";

       if (product is Clothing clothing)
       {

           if (Size != clothing.Size)
           {
                differences += Size;
           }

           if (Color != clothing.Color)
           {
               if(differences != "") differences += " | ";
               differences += Color;
           }
       }

       return differences;
    }

    public override string GetDifferentiations()
    {
        return Size + " | " + Color;
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj) && obj is Clothing clothing && Brand.ToCommonSyntax() == clothing.Brand.ToCommonSyntax() && Size.ToCommonSyntax() == clothing.Size.ToCommonSyntax() && Color.ToCommonSyntax() == clothing.Color.ToCommonSyntax() && Material.ToCommonSyntax() == clothing.Material.ToCommonSyntax();
    }
}