using Health.Net.Views;

namespace Health.Net
{
  public partial class MainWindow
  {
    public MainWindow(
      CreateFoodView createFoodView,
      FoodLogView foodLogView)
    {
      InitializeComponent();

      createFoodTabItem.Content = createFoodView;
      foodLogTabItem.Content = foodLogView;
    }
  }
}
