using DingWatGeldMaak.Core.Providers;
using System.Collections.Generic;

namespace DingWatGeldMaak.Core.Strategies
{
  public interface IStrategy
  {
    //IDataProvider DataProvider { get; set; }

    void Calculate();
    int Start();
    int Stop();
  }
}
