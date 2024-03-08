namespace SmartTradeLib.Entities;

public partial class Nutrition : Product
{
    public int Calories { get; set; }
    public int Proteins { get; set; }
    public int Carbohydrates { get; set; }
    public int Fats { get; set; }
    public string Allergens { get; set; }
    public int Weight { get; set; }
}