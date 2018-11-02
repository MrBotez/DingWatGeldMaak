using DingWatGeldMaak.Core.Providers;
using DingWatGeldMaak.FOREX.Collections;
using System.Collections.Generic;

namespace DingWatGeldMaak.FOREX.Strategies
{
  public interface IStrategy : Core.Strategies.IStrategy
  {
    ITimeSeries Data { get; }
    IEnumerable<ITimeSeries> Buffers { get; set; }
  }
}
