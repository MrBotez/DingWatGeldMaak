using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DingWatGeldMaak.FOREX.Markets
{
  public class Strategy : IDisposable
  {
    #region Properties

    /// <summary>
    /// The name of the strategy
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The market the strategy operates in
    /// </summary>
    public IMarket Market { get; private set; }

    /// <summary>
    /// The list of charts the strategy uses
    /// </summary>
    public Dictionary<string, List<Chart>> Charts { get; private set; }

    /// <summary>
    /// The time interval to check for new price data
    /// </summary>
    public TimeSpan Interval { get; set; }

    /// <summary>
    /// The time from which the data of the strategy must start
    /// </summary>
    public DateTime DataStartTime { get; set; }

    #endregion Properties

    protected Timer timer = null;
    protected bool isStopped = false;
    protected Dictionary<string, DateTime> lastDataTimes = null;

    public Strategy(IMarket market)
    {
      Market = market;
      isStopped = true;

      timer = new Timer(TimerCallback);
      Interval = TimeSpan.MaxValue;
      DataStartTime = DateTime.Now.AddMinutes(60 * 24 * 9);
      lastDataTimes = new Dictionary<string, DateTime>();

      Charts = new Dictionary<string, List<Chart>>();
    }

    public void Dispose()
    {
      if (timer != null)
      {
        timer.Change(Timeout.Infinite, Timeout.Infinite);
        timer.Dispose();
      }

      foreach (var key in Charts.Keys)
      {
        while (Charts[key].Count > 0)
        {
          var obj = Charts[key][0];
          Charts[key].RemoveAt(0);
          obj?.Dispose();
        }
      }

      lastDataTimes.Clear();
    }

    protected virtual void TimerCallback(object state)
    {
      timer.Change(Timeout.Infinite, Timeout.Infinite);

      try
      {
        if (!isStopped)
        {
          Parallel.ForEach(Charts.Keys, (key) =>
          {
            var data = Market.GetDataFromDate(key, lastDataTimes[key]);

            Parallel.ForEach(Charts[key], (item) => { item.UpdatePriceData(data); });

            lastDataTimes[key] = data.Last().Time;
          });
        }
      }
      catch (Exception ex)
      {
        //RaiseError("Something went wrong in retrieving data", ex);
      }

      if (!isStopped)
      {
        timer.Change(Interval, Interval);
      }
    }

    public Chart AddChart(string symbol, ChartTypeEnum chartType, ChartTimeFrameEnum timeFrame)
    {
      var comodityInfo = Market.GetCommodityInformation(symbol);
      var chart = new Chart(comodityInfo, chartType, timeFrame, Market);

      if (!Charts.ContainsKey(symbol))
      {
        Charts.Add(symbol, new List<Chart>());
        lastDataTimes.Add(symbol, DataStartTime);
      }

      Charts[symbol].Add(chart);

      return chart;
    }

    public void Start()
    {
      isStopped = false;

      timer.Change(TimeSpan.FromSeconds(0), Interval);  //Start immediately
    }

    public void Stop()
    {
      timer.Change(Timeout.Infinite, Timeout.Infinite);

      isStopped = true;
    }
  }
}
