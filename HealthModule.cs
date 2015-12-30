using System;
using Ninject.Modules;
using Health.Net.ViewModels;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.Win32;

namespace Health.Net
{
  public class HealthModule : NinjectModule
  {
    public override void Load()
    {
      var connFactory = new Func<SQLiteConnectionWithLock>(() =>
        new SQLiteConnectionWithLock(
          new SQLitePlatformWin32(),
          new SQLiteConnectionString(AppConfig.DbPath, storeDateTimeAsTicks: false)));

      Bind<SQLiteAsyncConnection>().ToConstant(new SQLiteAsyncConnection(connFactory) );
      Bind<MainWindow>().ToSelf().InSingletonScope();
      Bind<MainWindowViewModel>().ToSelf().InSingletonScope();
      Bind<AddFoodViewModel>().ToSelf().InSingletonScope();
    }
  }
}
