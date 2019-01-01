using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DingWatGeldMaak.Core.Providers;
using DingWatGeldMaak.FOREX.Charts;
using DingWatGeldMaak.FOREX.Indicators;
using DingWatGeldMaak.FOREX.Markets;

namespace DingWatGeldMaak.FOREX.Strategies
{
  public class SignalMatrix : Data.DataFrame<SignalEnum>
  {
    protected DateTime lastSignalTime = DateTime.MaxValue;

    public void SetSignal(string name, DateTime time, SignalEnum signal)
    {
      if (time < lastSignalTime) { lastSignalTime = time; }

      this[name][time] = signal;
    }
  }

  public class MovingAverageCrossOverStrategy : Strategy
  {
    public string Symbol { get; set; }

    protected SignalMatrix signalMatrix = null;

    /// <summary>
    /// Create a <see cref="MovingAverageCrossOverStrategy"/> object
    /// </summary>
    /// <param name="market">Access to the market</param>
    public MovingAverageCrossOverStrategy(IMarket market, string symbol) : base(market)
    {
      this.Symbol = symbol;
      signalMatrix = new SignalMatrix();

      var chart = this.AddChart(symbol, ChartTypeEnum.OHLC, ChartTimeFrameEnum.M15, $"{symbol}_M15");
      chart.AddIndicator(new MovingAverage(chart.ChartData, 20, MovingAverageMethodEnum.Simple, AppliesToEnum.Close, "Slow SMA"));
      chart.AddIndicator(new MovingAverage(chart.ChartData, 10, MovingAverageMethodEnum.Simple, AppliesToEnum.Close, "Fast SMA"));

      chart = this.AddChart(symbol, ChartTypeEnum.OHLC, ChartTimeFrameEnum.M30, $"{symbol}_M30");
      chart.AddIndicator(new MovingAverage(chart.ChartData, 20, MovingAverageMethodEnum.Simple, AppliesToEnum.Close, "Slow SMA"));
      chart.AddIndicator(new MovingAverage(chart.ChartData, 10, MovingAverageMethodEnum.Simple, AppliesToEnum.Close, "Fast SMA"));
    }

    protected override void ApplyStrategy()
    {
      /*
      Entry Conditions
        Enter Short position when FMA crosses SMA from above.
        Enter Long position when FMA crosses SMA from below.
       */

      #region Check the 15 minute chart

      var chartName = $"{Symbol}_M15";
      var slowIndicatorValues = Charts[Symbol].First(i => i.Name == chartName).GetIndicatorByName("Slow SMA").Data["Buffer"].GetPreviousData(lastDataTimes[Symbol], 2).ToList();
      var fastIndicatorValues = Charts[Symbol].First(i => i.Name == chartName).GetIndicatorByName("Fast SMA").Data["Buffer"].GetPreviousData(lastDataTimes[Symbol], 2).ToList();

      if (slowIndicatorValues.Count > 1)
      {
        if ((slowIndicatorValues[0].Value > fastIndicatorValues[0].Value) && (slowIndicatorValues[1].Value < fastIndicatorValues[1].Value))
        {
          //Short position: The fast moving average crossed the slow moving average from above
          signalMatrix.SetSignal(chartName, fastIndicatorValues[1].Key, SignalEnum.Sell);

          //Console.WriteLine($@"{slowIndicatorValues[0].Key:yyyy-MM-dd HH:mm:ss} : Short position identified");
        }
        else if ((slowIndicatorValues[0].Value < fastIndicatorValues[0].Value) && (slowIndicatorValues[1].Value > fastIndicatorValues[1].Value))
        {
          //Long position: The fast moving average crossed the slow moving average from below
          signalMatrix.SetSignal(chartName, fastIndicatorValues[1].Key, SignalEnum.Buy);

          //Console.WriteLine($@"{slowIndicatorValues[0].Key:yyyy-MM-dd HH:mm:ss} : Long position identified");
        }
        else
        {
          signalMatrix.SetSignal(chartName, fastIndicatorValues[1].Key, SignalEnum.None);
        }
      }

      #endregion Check the 15 minute chart

      #region Check the 30 minute chart

      chartName = $"{Symbol}_M30";
      slowIndicatorValues = Charts[Symbol].First(i => i.Name == chartName).GetIndicatorByName("Slow SMA").Data["Buffer"].GetPreviousData(lastDataTimes[Symbol], 2).ToList();
      fastIndicatorValues = Charts[Symbol].First(i => i.Name == chartName).GetIndicatorByName("Fast SMA").Data["Buffer"].GetPreviousData(lastDataTimes[Symbol], 2).ToList();

      if (slowIndicatorValues.Count > 1)
      {
        if ((slowIndicatorValues[0].Value > fastIndicatorValues[0].Value) && (slowIndicatorValues[1].Value < fastIndicatorValues[1].Value))
        {
          //Short position: The fast moving average crossed the slow moving average from above
          signalMatrix.SetSignal(chartName, fastIndicatorValues[1].Key, SignalEnum.Sell);

          //Console.WriteLine($@"{slowIndicatorValues[0].Key:yyyy-MM-dd HH:mm:ss} : Short position identified");
        }
        else if ((slowIndicatorValues[0].Value < fastIndicatorValues[0].Value) && (slowIndicatorValues[1].Value > fastIndicatorValues[1].Value))
        {
          //Long position: The fast moving average crossed the slow moving average from below
          signalMatrix.SetSignal(chartName, fastIndicatorValues[1].Key, SignalEnum.Buy);

          //Console.WriteLine($@"{slowIndicatorValues[0].Key:yyyy-MM-dd HH:mm:ss} : Long position identified");
        }
        else
        {
          signalMatrix.SetSignal(chartName, fastIndicatorValues[1].Key, SignalEnum.None);
        }
      }

      #endregion Check the 30 minute chart

      //lastDataTimes[]
      var m15 = signalMatrix[$"{Symbol}_M15"].DataDescending.FirstOrDefault();
      var m30 = signalMatrix[$"{Symbol}_M30"].DataDescending.FirstOrDefault();

      if ((m15.Value == m30.Value) && (m30.Value != SignalEnum.None))
      {
        if (m15.Key == m30.Key)
        {
          Console.WriteLine($@"{m30.Key:yyyy-MM-dd HH:mm:ss} : {Enum.GetName(typeof(SignalEnum), m30.Value)} position identified");
        }
      }
    }
  }
}
