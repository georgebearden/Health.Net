using Ninject.Modules;
using Health.Net.ViewModels;

namespace Health.Net
{
  public class HealthModule : NinjectModule
  {
    public override void Load()
    {
      Bind<MainWindow>().ToSelf().InSingletonScope();

      Bind<MainWindowViewModel>().ToSelf().InSingletonScope();
      Bind<AddFoodViewModel>().ToSelf().InSingletonScope();
    }
  }
}
