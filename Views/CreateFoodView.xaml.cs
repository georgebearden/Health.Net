using Health.Net.ViewModels;
using ReactiveUI;

namespace Health.Net.Views
{
  public partial class CreateFoodView : IViewFor<CreateFoodViewModel>
  {
    public CreateFoodView(CreateFoodViewModel viewModel)
    {
      InitializeComponent();

      ViewModel = viewModel;

      this.WhenActivated(d =>
      {
        d(this.Bind(ViewModel, vm => vm.Name, v => v.nameTextBox.Text));
        d(this.OneWayBind(ViewModel, vm => vm.FoodGroups, v => v.foodGroupsComboBox.ItemsSource));
        d(this.Bind(ViewModel, vm => vm.SelectedFoodGroup, v => v.foodGroupsComboBox.SelectedItem));
        d(this.Bind(ViewModel, vm => vm.Calories, v => v.caloriesTextBox.Text));
        d(this.Bind(ViewModel, vm => vm.CaloriesFromFat, v => v.caloriesFromFatTextBox.Text));
        d(this.Bind(ViewModel, vm => vm.Fat, v => v.fatTextBox.Text));
        d(this.Bind(ViewModel, vm => vm.Cholesterol, v => v.cholesterolTextBox.Text));
        d(this.Bind(ViewModel, vm => vm.Sodium, v => v.sodiumTextBox.Text));
        d(this.Bind(ViewModel, vm => vm.Carbohydrates, v => v.carbohydratesTextBox.Text));
        d(this.Bind(ViewModel, vm => vm.Fiber, v => v.fiberTextBox.Text));
        d(this.Bind(ViewModel, vm => vm.Sugar, v => v.sugarTextBox.Text));
        d(this.Bind(ViewModel, vm => vm.Protein, v => v.proteinTextBox.Text));
        d(this.BindCommand(ViewModel, vm => vm.CreateFood, v => v.createFoodButton));
      });
    }

    object IViewFor.ViewModel
    {
      get { return ViewModel; }
      set { ViewModel = (CreateFoodViewModel)value; }
    }

    public CreateFoodViewModel ViewModel { get; set; }
  }
}
