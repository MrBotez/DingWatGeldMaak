using DingWatGeldMaak.Core.Providers;
using DingWatGeldMaak.FOREX.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DingWatGeldMaak.FOREX.Providers
{
  public class HistoryDataProvider : PriceDataProvider // TimeIntervalDataProvider<OHLC>
  {
    protected string fileName = "";
    protected int currentIndex = 0;

    public HistoryDataProvider(string fileName) : base()
    {
      this.fileName = fileName;
    }

    public override void Dispose()
    {
      base.Dispose();
    }

    public override void Start()
    {
      currentIndex = 0;
      historyData.Clear();

      if (File.Exists(fileName))
      {
        foreach (var line in File.ReadAllLines(fileName))
        {
          var flds = line.Split(',');

          var dateComponents = flds[0].Split('.').Select(i => Convert.ToInt32(i)).ToList();
          dateComponents.AddRange(flds[1].Split(':').Select(i => Convert.ToInt32(i)));

          historyData.Add(new OHLC()
            .SetTime(new System.DateTime(dateComponents[0], dateComponents[1], dateComponents[2], dateComponents[3], dateComponents[4], 0))
            .SetOpen(Convert.ToDouble(flds[2]))
            .SetHigh(Convert.ToDouble(flds[3]))
            .SetLow(Convert.ToDouble(flds[4]))
            .SetClose(Convert.ToDouble(flds[5]))
            .SetVolume(Convert.ToInt32(flds[6]))
          );
        }
      }

      base.Start();
    }

    public override void Get()
    {
      if (currentIndex < historyData.Count)
      {
        var data = new List<OHLC>() { historyData[currentIndex] };

        RaiseDataAvailable(data);

        currentIndex++;
      }
      else
      {
        RaiseError("No more data", null);
      }
    }
  }
}
