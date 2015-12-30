using Health.Net.ViewModels;
using ReactiveUI;

namespace Health.Net.Views
{
  public partial class FoodLogView : IViewFor<FoodLogViewModel>
  {
    public FoodLogView(FoodLogViewModel viewModel)
    {
      InitializeComponent();

      ViewModel = viewModel;

      this.WhenActivated(d =>
      {

      });
    }

    object IViewFor.ViewModel
    {
      get { return ViewModel; }
      set { ViewModel = (FoodLogViewModel)value; }
    }

    public FoodLogViewModel ViewModel { get; set; }
  }
}
