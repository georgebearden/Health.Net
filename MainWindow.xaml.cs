using System;
using Health.Net.Views;

namespace Health.Net
{
  public partial class MainWindow
  {
    readonly MainWindowViewModel mainWindowViewModel;
    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
      InitializeComponent();

      this.mainWindowViewModel = mainWindowViewModel;
      Closing += OnClosing;

      var addFoodView = new AddFoodView {ViewModel = mainWindowViewModel.AddFoodViewModel};
      addFoodTabItem.Content = addFoodView;
    }

    void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      mainWindowViewModel.Dispose();
    }
  }
}
