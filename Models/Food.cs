using SQLite.Net.Attributes;

namespace Health.Net.Models
{
  public class Food
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public FoodGroups FoodGroup { get; set; }
    public double Calories { get; set; }
    public double CaloriesFromFat { get; set; }
    public double FatGrams { get; set; }
    public double CholesterolMilligrams { get; set; }
    public double SodiumMilligrams { get; set; }
    public double CarbohydrateGrams { get; set; }
    public double FiberGrams { get; set; }
    public double SugarGrams { get; set; }
    public double ProteinGrams { get; set; }
  }
}
