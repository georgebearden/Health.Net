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
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      var kernel = new StandardKernel(new HealthModule());

      
      Task.Run(async () =>
      {
        // Do not recreate the database if it exists.
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
      });

      Current.MainWindow = kernel.Get<MainWindow>();
      Current.MainWindow.ShowDialog();
    }
  }
}
