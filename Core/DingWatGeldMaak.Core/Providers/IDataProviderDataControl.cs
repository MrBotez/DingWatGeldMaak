using DingWatGeldMaak.Core.Events;
using System;
using System.Collections.Generic;

namespace DingWatGeldMaak.Core.Providers
{
  public interface IDataProviderDataControl<T>
  {
    IEnumerable<T> GetDataFromDate(DateTime start);
    event DataAvailableHandler<T> OnDataAvailable;
  }
}
