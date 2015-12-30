using ReactiveUI;
using System;
using Health.Net.ViewModels;

namespace Health.Net
{
  public class MainWindowViewModel : ReactiveObject, IDisposable
  {
    public MainWindowViewModel(AddFoodViewModel addFoodViewModel)
    {
      AddFoodViewModel = addFoodViewModel;
    }

    public void Dispose()
    {
      AddFoodViewModel.Dispose();
    }

    AddFoodViewModel addFoodViewModel;
    public AddFoodViewModel AddFoodViewModel
    {
      get { return addFoodViewModel; }
      set { this.RaiseAndSetIfChanged(ref addFoodViewModel, value); }
    }
  }
}
