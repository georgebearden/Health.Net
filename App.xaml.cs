using System.Windows;
using Ninject;

namespace Health.Net
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      var kernel = new StandardKernel(new HealthModule());
      
      Current.MainWindow = kernel.Get<MainWindow>();
      Current.MainWindow.ShowDialog();
    }
  }
}
