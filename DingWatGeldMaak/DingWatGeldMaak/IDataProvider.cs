using System;
using System.Linq;
using System.Text;

namespace DingWatGeldMaak
{
  public interface IDataProvider : IDisposable
  {
    string Name { get; set; }

    void Start();
    void Stop();
    void Get();

    event DataAvailableHandler OnDataAvailable;
    event ErrorOccurredHandler OnError;
  }
}
