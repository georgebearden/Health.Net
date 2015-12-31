using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Health.Net.Models;
using Ninject;
using SQLite.Net.Async;

namespace Health.Net
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App
  {
    MainWindow mainWindow;

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      var kernel = new StandardKernel(new HealthModule());

      Task.Run(async () =>
      {
        // Do not recreate the database if it exists. (and prob. also if the schemas are the same...).
        if (File.Exists(AppConfig.DbPath))
        {
          var backupFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Health.Net");
          if (!Directory.Exists(backupFolder))
            Directory.CreateDirectory(backupFolder);

          var backupPath = Path.Combine(backupFolder, $"{DateTime.Now.Ticks}-Health.db");
          File.Copy(AppConfig.DbPath, backupPath, true);
          return;
        }
          
        var sqlite = kernel.Get<SQLiteAsyncConnection>();
        await sqlite.CreateTableAsync<Food>();
        await sqlite.CreateTableAsync<FoodLog>();
      });

      mainWindow = kernel.Get<MainWindow>();
      Current.MainWindow = mainWindow;
      Current.MainWindow.ShowDialog();
    }

    protected override void OnExit(ExitEventArgs e)
    {
      mainWindow.Dispose();

      base.OnExit(e);
    }
  }
}
