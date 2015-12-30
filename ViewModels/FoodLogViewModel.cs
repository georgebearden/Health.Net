using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Health.Net.Models;
using ReactiveUI;
using SQLite.Net.Async;

namespace Health.Net.ViewModels
{
  public class FoodLogViewModel : ReactiveObject
  {
    readonly SQLiteAsyncConnection sqlite;
    public FoodLogViewModel(SQLiteAsyncConnection sqlite)
    {
      this.sqlite = sqlite;

      var canLogIt = this.WhenAny(
        vm => vm.selectedDate,
        date => true
        );

      LogIt = ReactiveCommand.CreateAsyncTask(canLogIt, LogItImpl);
      Foods = new ReactiveList<string>();

      SelectedDate = DateTime.Now;
    }

    async Task LogItImpl(object _)
    {
      var newFoodLogs = new List<FoodLog>();

      foreach (var food in Foods)
      {
        var foodId =  sqlite.Table<Food>()
          .Where(f => string.Compare(f.Name, food, StringComparison.InvariantCultureIgnoreCase) == 0)
          .FirstAsync()
          .Id;

        var newFoodLog = new FoodLog
        {
          // ReSharper disable once PossibleInvalidOperationException
          Date = SelectedDate.Value,
          Food = foodId
        };

        newFoodLogs.Add(newFoodLog);
      }

      await sqlite.InsertAllAsync(newFoodLogs);
    }

    DateTime? selectedDate;
    public DateTime? SelectedDate
    {
      get { return selectedDate; }
      set { this.RaiseAndSetIfChanged(ref selectedDate, value); }
    }

    public ReactiveList<string> Foods { get; } 
    public ReactiveCommand<Unit> LogIt { get; }
  }
}
