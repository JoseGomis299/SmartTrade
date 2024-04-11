using SmartTrade.BusinessLogic;

namespace SmartTrade.Entities;

public partial class Toy : Product
{
    public Toy(){}
    //public Toy(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string brand, string material) : base(name, certification, ecologicPrint, minimumAge, howToUse, howToReducePrint)
    //{
    //    Brand = brand;
    //    Material = material;
    //}

    public override string GetInfo()
    {
        return $"- Brand: {Brand}" +
               $"\n- Material: {Material}";
    }

    public override string[] GetAttributes()
    {
        return new[] { Brand, Material };
    }

    public override ICollection<Category> GetCategories()
    {
        return new List<Category> { Category.Toy };
    }

    public override string GetDifferences(Product product)
    {
        string differences = "";

        if (product is Toy toy)
        {
            if (Material != toy.Material)
            {
                differences += Material;
            }
        }

        return differences;
    }

    public override string GetDifferentiations()
    {
        return Material;
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj) && obj is Toy toy && Brand.ToCommonSyntax() == toy.Brand.ToCommonSyntax() && Material.ToCommonSyntax() == toy.Material.ToCommonSyntax();
    }
}