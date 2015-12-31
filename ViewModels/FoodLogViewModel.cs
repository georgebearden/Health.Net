using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Health.Net.Models;
using ReactiveUI;
using SQLite.Net.Async;

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

      FindIt = ReactiveCommand.CreateAsyncTask(FindItImpl);
      FindIt.Subscribe(async foodId =>
      {
        if (foodId == 0)
          return;

        var newFoodLog = new FoodLog
        {
          // ReSharper disable once PossibleInvalidOperationException
          Date = SelectedDate.Value,
          Food = foodId
        };

        await sqlite.InsertAsync(newFoodLog);

        // todo instead of resetting the text to empty, it would be nice to give the 
        // user some other type of feedback, like maybe a subtle created indicator that fades or something.
        Food = string.Empty;
      });

      var canLogIt = this.WhenAny(
        x => x.Food,
        food => !string.IsNullOrEmpty(food.Value));
      LogIt = ReactiveCommand.CreateAsyncTask(canLogIt, _ => LogItImpl());
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

    async Task LogItImpl()
    {
      FindIt.Execute(null);
      await Task.CompletedTask;
    }
  }
}