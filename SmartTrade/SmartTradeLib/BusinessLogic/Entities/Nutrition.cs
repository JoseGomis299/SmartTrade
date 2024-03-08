namespace SmartTradeLib.Entities;

public partial class Nutrition : Product
{
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