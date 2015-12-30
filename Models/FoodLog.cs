using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Health.Net.Models
{
  public class FoodLog
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public DateTime Date { get; set; }

    [ForeignKey(typeof(Food))]
    public int Food { get; set; }
  }
}
