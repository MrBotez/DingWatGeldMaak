using DingWatGeldMaak.Core.Providers;
using System;
using System.Collections.Generic;

namespace DingWatGeldMaak.Core.Strategies
{
  public class Strategy : IStrategy
  {
    public IDataProvider DataProvider { get; set; }

    /// <summary>
    /// Indicates wether the startegy is stopped
    /// </summary>
    protected bool isStopped;

    public Strategy(IDataProvider provider)
    {
      isStopped = true;

      DataProvider = provider;
      provider.OnDataAvailable += Provider_OnDataAvailable;
    }

    protected void Provider_OnDataAvailable(object sender, object data)
    {
      try
      {
        ProcessDataAvailable(data);
      }
      catch { }

      Calculate();
    }

    protected virtual void ProcessDataAvailable(object data)
    {
      
    }

    public virtual void Calculate()
    {

    }

    public virtual int Start()
    {
      isStopped = false;

      return 0;
    }

    public virtual int Stop()
    {
      isStopped = true;

      return 0;
    }
  }
}
