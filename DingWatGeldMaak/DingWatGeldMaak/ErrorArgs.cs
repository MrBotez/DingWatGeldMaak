using System;

namespace DingWatGeldMaak
{
  public delegate void DataAvailableHandler(object sender, object data);

  public class ErrorArgs
  {
    public string Message { get; set; }
    public Exception Exception { get; set; }
  }

  public delegate void ErrorOccurredHandler(object sender, ErrorArgs args);
}
