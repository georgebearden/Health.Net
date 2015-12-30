using System;
using Ninject.Modules;
using Health.Net.ViewModels;
using Health.Net.Views;
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
      Bind<CreateFoodView>().ToSelf().InSingletonScope();
      Bind<CreateFoodViewModel>().ToSelf().InSingletonScope();
      Bind<FoodLogView>().ToSelf().InSingletonScope();
      Bind<FoodLogViewModel>().ToSelf().InSingletonScope();
    }
  }
}
