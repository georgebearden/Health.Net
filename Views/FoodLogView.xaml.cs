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
        d(this.Bind(ViewModel, vm => vm.SelectedDate, v => v.datePicker.SelectedDate));
        d(this.Bind(ViewModel, vm => vm.Food, v => v.foodTextBox.Text));
        d(this.BindCommand(ViewModel, vm => vm.FindIt, v => v.findItButton));
        d(this.BindCommand(ViewModel, vm => vm.LogIt, v => v.logItButton));
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
