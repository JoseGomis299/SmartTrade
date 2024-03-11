namespace SmartTradeLib.Entities;

public partial class Nutrition : Product
{
    public Nutrition(string name, string certification, string ecologicPrint, int minimumAge, bool validated, byte[] image, Post post, string calories, string proteins, string carbohydrates, string fats, string allergens, string weight) : base(name, certification, ecologicPrint, minimumAge, validated, image, post)
    {
        Calories = calories;
        Proteins = proteins;
        Carbohydrates = carbohydrates;
        Fats = fats;
        Allergens = allergens;
        Weight = weight;
    }

    public override string GetInfo()
    {
        return $"- Calories: {Calories}kcal" +
               $"\n- Proteins: {Proteins}g" +
               $"\n- Carbohydrates: {Carbohydrates}g" +
               $"\n- Fats: {Fats}g" +
               $"\n- Allergens: {Allergens}g" +
               $"\n- Weight: {Weight}g";
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
}