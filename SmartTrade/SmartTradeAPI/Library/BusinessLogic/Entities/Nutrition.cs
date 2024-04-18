using SmartTrade.BusinessLogic;

namespace SmartTrade.Entities;

public partial class Nutrition : Product
{
    public Nutrition() { }

    //public Nutrition(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string weight, string calories, string proteins, string carbohydrates, string fats, string allergens) : base(name, certification, ecologicPrint, minimumAge, howToUse, howToReducePrint)
    //{
    //    Calories = calories;
    //    Proteins = proteins;
    //    Carbohydrates = carbohydrates;
    //    Fats = fats;
    //    Allergens = allergens;
    //    Weight = weight;
    //}

    public override string GetInfo()
    {
        return $"- Weight: {Weight}g" +
               $"\n- Calories: {Calories}kcal" +
               $"\n- Proteins: {Proteins}g" +
               $"\n- Carbohydrates: {Carbohydrates}g" +
               $"\n- Fats: {Fats}g" +
               $"\n- Allergens: {Allergens}g";

    }

    public override Dictionary<string, string> GetAttributes()
    {
        return new Dictionary<string, string>
        {
            { nameof(Weight), Weight },
            { nameof(Calories), Calories },
            { nameof(Proteins), Proteins },
            { nameof(Carbohydrates), Carbohydrates },
            { nameof(Fats), Fats },
            { nameof(Allergens), Allergens }
        };
    }

    public override ICollection<Category> GetCategories()
    {
        return new List<Category> { Category.Nutrition };
    }

    public override string GetDifferences(Product product)
    {
        string differences = "";

        if (product is Nutrition nutrition)
        {
            if (Weight != nutrition.Weight)
                differences += $"{Weight}g";
        }  
        
        return differences;
    }

    public override string GetDifferentiations()
    {
        return $"{Weight}g";
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj) && obj is Nutrition nutrition 
                                && Calories.ToCommonSyntax() == nutrition.Calories.ToCommonSyntax() 
                                && Proteins.ToCommonSyntax() == nutrition.Proteins.ToCommonSyntax() 
                                && Carbohydrates.ToCommonSyntax() == nutrition.Carbohydrates.ToCommonSyntax() 
                                && Fats.ToCommonSyntax() == nutrition.Fats.ToCommonSyntax() 
                                && Allergens.ToCommonSyntax() == nutrition.Allergens.ToCommonSyntax() 
                                && Weight.ToCommonSyntax() == nutrition.Weight.ToCommonSyntax();
    }
}