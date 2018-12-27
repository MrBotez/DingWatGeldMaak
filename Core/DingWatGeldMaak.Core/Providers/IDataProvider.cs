using DingWatGeldMaak.Core.Events;
using System;
using System.Linq;
using System.Text;

namespace DingWatGeldMaak.Core.Providers
{
  public interface IDataProvider<T> : IDisposable, IDataProviderDataControl<T>
  {
    string Name { get; set; }

    void Start();
    void Stop();
    void Get();

    event ErrorOccurredHandler OnError;
  }
}
