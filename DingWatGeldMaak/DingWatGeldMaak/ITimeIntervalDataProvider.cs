using System;

namespace DingWatGeldMaak
{
  public interface ITimeIntervalDataProvider
  {
    TimeSpan Interval { get; set; }
  }
}
