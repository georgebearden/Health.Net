using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Health.Net.ViewModels;

namespace Health.Net
{
  public class MainWindowViewModel : ReactiveObject
  {

    AddFoodViewModel addFoodViewModel;
    public AddFoodViewModel AddFoodViewModel
    {
      get { return addFoodViewModel; }
      set { this.RaiseAndSetIfChanged(ref addFoodViewModel, value); }
    }
  }
}
