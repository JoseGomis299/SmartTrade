namespace SmartTradeLib.Entities;

public partial class Toy : Product
{
    public Toy(){}
    public Toy(string name, string certification, string ecologicPrint, int minimumAge, string brand, string material, string age) : base(name, certification, ecologicPrint, minimumAge)
    {
        Brand = brand;
        Material = material;
        Age = age;
    }

    public override string GetInfo()
    {
        return $"- Brand: {Brand}" +
               $"\n- Material: {Material}" +
               $"\n- Age: {Age}";
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
}