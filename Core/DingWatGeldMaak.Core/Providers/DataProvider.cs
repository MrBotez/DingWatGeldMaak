using DingWatGeldMaak.Core.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DingWatGeldMaak.Core.Providers
{
  public class DataProvider<T> : IDataProvider<T>
  {
    public string Name { get; set; }

    public event DataAvailableHandler<T> OnDataAvailable;
    public event ErrorOccurredHandler OnError;

    protected bool isStopped = false;

    public DataProvider()
    {
      isStopped = true;
    }

    public virtual void Dispose()
    {
      if (!isStopped) { Stop(); }
    }

    public virtual void Get()
    {

    }

    public virtual void Start()
    {
      isStopped = false;
    }

    public virtual void Stop()
    {
      isStopped = true;
    }

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

    public virtual IEnumerable<T> GetDataFromDate(DateTime start)
    {
      return null;
    }
  }
}
