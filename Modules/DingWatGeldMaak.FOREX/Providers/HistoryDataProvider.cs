using DingWatGeldMaak.Core.Providers;
using DingWatGeldMaak.FOREX.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DingWatGeldMaak.FOREX.Providers
{
  public class HistoryDataProvider : PriceDataProvider
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

          var date = new System.DateTime(dateComponents[0], dateComponents[1], dateComponents[2], dateComponents[3], dateComponents[4], 0);
          var ohlc = new OHLC()
            .SetTime(date)
            .SetOpen(Convert.ToDouble(flds[2]))
            .SetHigh(Convert.ToDouble(flds[3]))
            .SetLow(Convert.ToDouble(flds[4]))
            .SetClose(Convert.ToDouble(flds[5]))
            .SetVolume(Convert.ToInt32(flds[6]));

          historyData.Add(ohlc);
          dataIndex.Add(date, ohlc);
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

    public override IEnumerable<OHLC> GetDataFromDate(DateTime start)
    {
      OHLC obj = null;
      if (dataIndex.ContainsKey(start))
      {
        obj = dataIndex[start];
      }

      if (obj == null)
      {
        if (historyData.Count > 0)
        {
          return new OHLC[1] { historyData.First() };
        }
        else
        {
          return new OHLC[0];
        }
      }
      else
      {
        var index = historyData.IndexOf(obj);

        if (index == historyData.Count - 1)
        {
          return new OHLC[1] { obj };
        }
        else
        {
          return new OHLC[2] { obj, historyData[index + 1] };
        }
      }
    }
  }
}
