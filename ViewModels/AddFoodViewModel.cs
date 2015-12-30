﻿using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Health.Net.Models;
using ReactiveUI;

namespace Health.Net.ViewModels
{
  public class AddFoodViewModel : ReactiveObject, IDisposable
  {

    public AddFoodViewModel()
    {
      FoodGroups = new ReactiveList<FoodGroups>();
      foreach (var foodGroup in Enum.GetValues(typeof(FoodGroups)).Cast<FoodGroups>())
        FoodGroups.Add(foodGroup);

      var canCreateFood = this.WhenAny(
                x => x.Name,
                fileName => !string.IsNullOrEmpty(fileName.Value));

      CreateFood = ReactiveCommand.CreateAsyncTask(canCreateFood, OnCreateFood);
    }

    Task OnCreateFood(object arg)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
    }

    public ReactiveList<FoodGroups> FoodGroups { get; }

    string name;
    public string Name
    {
      get { return name; }
      set { this.RaiseAndSetIfChanged(ref name, value); }
    }

    FoodGroups selectedFoodGroup;
    public FoodGroups SelectedFoodGroup
    {
      get { return selectedFoodGroup; }
      set { this.RaiseAndSetIfChanged(ref selectedFoodGroup, value); }
    }

    double calories;
    public double Calories
    {
      get { return calories; }
      set { this.RaiseAndSetIfChanged(ref calories, value); }
    }

    double caloriesFromFat;
    public double CaloriesFromFat
    {
      get { return caloriesFromFat; }
      set { this.RaiseAndSetIfChanged(ref caloriesFromFat, value); }
    }

    double fat;
    public double Fat
    {
      get { return fat; }
      set { this.RaiseAndSetIfChanged(ref fat, value); }
    }

    double cholesterol;
    public double Cholesterol
    {
      get { return cholesterol; }
      set { this.RaiseAndSetIfChanged(ref cholesterol, value); }
    }

    double sodium;
    public double Sodium
    {
      get { return sodium; }
      set { this.RaiseAndSetIfChanged(ref sodium, value); }
    }

    double carbohydrates;
    public double Carbohydrates
    {
      get { return carbohydrates; }
      set { this.RaiseAndSetIfChanged(ref carbohydrates, value); }
    }

    double fiber;
    public double Fiber
    {
      get { return fiber; }
      set { this.RaiseAndSetIfChanged(ref fiber, value); }
    }

    double sugar;
    public double Sugar
    {
      get { return sugar; }
      set { this.RaiseAndSetIfChanged(ref sugar, value); }
    }

    double protein;
    public double Protein
    {
      get { return protein; }
      set { this.RaiseAndSetIfChanged(ref protein, value); }
    }

    public ReactiveCommand<Unit> CreateFood { get; private set; }
  }
}
