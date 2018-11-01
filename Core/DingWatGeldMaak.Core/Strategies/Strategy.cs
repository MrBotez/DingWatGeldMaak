using DingWatGeldMaak.Core.Providers;
using System.Collections.Generic;

namespace DingWatGeldMaak.Core.Strategies
{
  public class Strategy : IStrategy
  {
    public IDataProvider DataProvider { get; set; }

    public Strategy(IDataProvider provider)
    {
      DataProvider = provider;
      provider.OnDataAvailable += Provider_OnDataAvailable;
    }

    protected virtual void Provider_OnDataAvailable(object sender, object data)
    {
      Calculate();
    }

    public virtual void Calculate()
    {

    }
  }
}
