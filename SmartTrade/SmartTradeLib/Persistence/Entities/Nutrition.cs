namespace SmartTrade.Entities;

public partial class Nutrition : Product
{
    public string Calories { get; set; }
    public string Proteins { get; set; }
    public string Carbohydrates { get; set; }
    public string Fats { get; set; }
    public string Allergens { get; set; }
    public string Weight { get; set; }
}