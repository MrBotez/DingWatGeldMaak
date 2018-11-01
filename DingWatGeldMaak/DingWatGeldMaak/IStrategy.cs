using System.Collections.Generic;

namespace DingWatGeldMaak
{
  public interface IStrategy
  {
    IDataProvider DataProvider { get; set; }
    IEnumerable<ITimeSeries> Buffers { get; set; }

    void Calculate();
  }
}
