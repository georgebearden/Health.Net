using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Health.Net.Views;
using MahApps.Metro.Controls.Dialogs;

namespace Health.Net
{
  public partial class MainWindow : IDisposable
  {
    readonly IDisposable disposable;
    readonly CreateFoodView createFoodView;
    readonly FoodLogView foodLogView;

    public MainWindow(
      CreateFoodView createFoodView,
      FoodLogView foodLogView)
    {
      InitializeComponent();

      this.createFoodView = createFoodView;
      this.foodLogView = foodLogView;

      createFoodTabItem.Content = createFoodView;
      foodLogTabItem.Content = foodLogView;

      disposable = foodLogView.ViewModel.FindIt.Where(id => id == 0).Subscribe(async _ => await ShowCreateFoodDialog());
    }
    public void Dispose()
    {
      disposable.Dispose();
    }

    async Task ShowCreateFoodDialog()
    {
      var settings = new MetroDialogSettings
      {
        AffirmativeButtonText = "Yes",
        NegativeButtonText = "No",
        AnimateShow = false,
        AnimateHide = false
      };

      var response = await this.ShowMessageAsync(string.Empty, 
        "That food does not exist.  Would you like to create it now?",
        MessageDialogStyle.AffirmativeAndNegative, settings);

      if (response == MessageDialogResult.Affirmative)
      {
        createFoodView.ViewModel.Name = foodLogView.ViewModel.Food;
        tabs.SelectedIndex = 1;
      }
    }
  }
}
