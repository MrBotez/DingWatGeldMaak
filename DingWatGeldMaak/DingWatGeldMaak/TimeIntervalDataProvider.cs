using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DingWatGeldMaak
{
  public class TimeIntervalDataProvider<T> : ITimeIntervalDataProvider
  {
    public TimeSpan Interval { get; set; }
    public string Name { get; set; }

    public event DataAvailableHandler OnDataAvailable;
    public event ErrorOccurredHandler OnError;

    protected bool isStopped = false;
    protected Timer timer = null;

    protected void RaiseError(string message, Exception exception)
    {
      var h = OnError;
      if (h != null)
      {
        Task.Run(() => { h(this, new ErrorArgs() { Exception = exception, Message = message }); });
      }
    }

    protected void RaiseDataAvailable(IEnumerable<T> data)
    {
      var h = OnDataAvailable;
      if (h != null)
      {
        Task.Run(() => { h(this, data); });
      }
    }

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

    public TimeIntervalDataProvider()
    {
      timer = new Timer(TimerCallback);
      Interval = TimeSpan.MaxValue;
      isStopped = true;
    }

    public virtual void Get()
    {
      RaiseDataAvailable(null);
    }

    public virtual void Start()
    {
      isStopped = false;
      timer.Change(TimeSpan.FromSeconds(0), Interval);  //Start immediately
    }

    public virtual void Stop()
    {
      isStopped = true;
      timer.Change(Timeout.Infinite, Timeout.Infinite);
    }

    public void Dispose()
    {
      if (!isStopped) { Stop(); }

      if (timer != null)
      {
        timer.Change(Timeout.Infinite, Timeout.Infinite);
        timer.Dispose();
      }
    }
  }

  //public class FileDataProvider : IFileDataProvider
  //{

  //}
}
