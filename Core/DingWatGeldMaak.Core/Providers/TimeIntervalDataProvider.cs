using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DingWatGeldMaak.Core.Providers
{
  public class TimeIntervalDataProvider<T> : DataProvider<T>, ITimeIntervalDataProvider
  {
    public TimeSpan Interval { get; set; }

    protected Timer timer = null;

    protected virtual void TimerCallback(object state)
    {
      timer.Change(Timeout.Infinite, Timeout.Infinite);

      try
      {
        if (!isStopped)
        {
          Get();
        }
      }
      catch (Exception ex)
      {
        RaiseError("Something went wrong in retrieving data", ex);
      }

      if (!isStopped)
      {
        timer.Change(Interval, Interval);
      }
    }

    public TimeIntervalDataProvider() : base()
    {
      timer = new Timer(TimerCallback);
      Interval = TimeSpan.MaxValue;
    }

    public override void Start()
    {
      base.Start();

      timer.Change(TimeSpan.FromSeconds(0), Interval);  //Start immediately
    }

    public override void Stop()
    {
      base.Stop();

      timer.Change(Timeout.Infinite, Timeout.Infinite);
    }

    public override void Dispose()
    {
      base.Dispose();

      if (timer != null)
      {
        timer.Change(Timeout.Infinite, Timeout.Infinite);
        timer.Dispose();
      }
    }
  }
}
