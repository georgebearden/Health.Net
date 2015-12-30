using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Health.Net.Models;
using ReactiveUI;
using SQLite.Net.Async;
using System.Linq;

namespace Health.Net.ViewModels
{
  public class FoodLogViewModel : ReactiveObject
  {
    readonly SQLiteAsyncConnection sqlite;
    string food;
    DateTime? selectedDate;

    public FoodLogViewModel(SQLiteAsyncConnection sqlite)
    {
      this.sqlite = sqlite;
      SelectedDate = DateTime.Now;

      var canFindIt = this.WhenAny(
        x => x.Food,
        food => !string.IsNullOrEmpty(food.Value));
      FindIt = ReactiveCommand.CreateAsyncTask(canFindIt, FindItImpl);

      // LogIt can execute whenever FindIt returns a number other than 0.
      var canLogIt = FindIt.Select(Convert.ToBoolean);
      LogIt = ReactiveCommand.CreateAsyncTask(canLogIt, LogItImpl);
    }

    public DateTime? SelectedDate
    {
      get { return selectedDate; }
      set { this.RaiseAndSetIfChanged(ref selectedDate, value); }
    }

    public string Food
    {
      get { return food; }
      set { this.RaiseAndSetIfChanged(ref food, value); }
    }

    public ReactiveCommand<int> FindIt { get; }
    public ReactiveCommand<Unit> LogIt { get; }

    async Task<int> FindItImpl(object _)
    {
      var foundFood = await sqlite.Table<Food>()
        .Where(f => f.Name == Food)
        .FirstOrDefaultAsync();

      return foundFood?.Id ?? 0;
    }

    async Task LogItImpl(object _)
    {
      var foodId = sqlite.Table<Food>()
        .Where(f => string.Compare(f.Name, food, StringComparison.InvariantCultureIgnoreCase) == 0)
        .FirstAsync()
        .Id;

      var newFoodLog = new FoodLog
      {
        // ReSharper disable once PossibleInvalidOperationException
        Date = SelectedDate.Value,
        Food = foodId
      };

      await sqlite.InsertAsync(newFoodLog);
    }
  }
}