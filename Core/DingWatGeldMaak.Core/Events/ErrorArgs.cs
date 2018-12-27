using System;
using System.Collections.Generic;

namespace DingWatGeldMaak.Core.Events
{
  public delegate void DataAvailableHandler<T>(object sender, IEnumerable<T> data);

  public class ErrorArgs
  {
    public string Message { get; set; }
    public Exception Exception { get; set; }
  }

  public delegate void ErrorOccurredHandler(object sender, ErrorArgs args);
}
