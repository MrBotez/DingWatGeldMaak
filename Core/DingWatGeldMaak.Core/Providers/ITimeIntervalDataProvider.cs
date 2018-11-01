using System;

namespace DingWatGeldMaak.Core.Providers
{
  public interface ITimeIntervalDataProvider
  {
    TimeSpan Interval { get; set; }
  }
}
