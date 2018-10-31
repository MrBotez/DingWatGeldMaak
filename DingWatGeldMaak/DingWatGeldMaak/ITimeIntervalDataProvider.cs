using System;

namespace DingWatGeldMaak
{
  public interface ITimeIntervalDataProvider : IDataProvider
  {
    TimeSpan Interval { get; set; }
  }

  //public class FileDataProvider : IFileDataProvider
  //{

  //}
}
