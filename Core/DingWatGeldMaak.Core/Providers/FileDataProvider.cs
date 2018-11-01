using DingWatGeldMaak.Core.Events;

namespace DingWatGeldMaak.Core.Providers
{
  public class FileDataProvider : IFileDataProvider
  {
    public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public event DataAvailableHandler OnDataAvailable;
    public event ErrorOccurredHandler OnError;

    public void Dispose()
    {

    }

    public void Get()
    {

    }

    public void Start()
    {

    }

    public void Stop()
    {

    }
  }
}
