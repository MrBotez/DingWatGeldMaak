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
  public class MovingAverageCrossOverStrategy : Strategy
  {
    public string Symbol { get; set; }

    /// <summary>
    /// Create a <see cref="MovingAverageCrossOverStrategy"/> object
    /// </summary>
    /// <param name="market">Access to the market</param>
    public MovingAverageCrossOverStrategy(IMarket market, string symbol) : base(market)
    {
      this.Symbol = symbol;

      var chart = this.AddChart(symbol, ChartTypeEnum.OHLC, ChartTimeFrameEnum.M15);
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

      var slowIndicatorValues = Charts[Symbol][0].GetIndicatorByName("Slow SMA").Data["Buffer"].GetPreviousData(lastDataTimes[Symbol], 2).ToList();
      var fastIndicatorValues = Charts[Symbol][0].GetIndicatorByName("Fast SMA").Data["Buffer"].GetPreviousData(lastDataTimes[Symbol], 2).ToList();

      if (slowIndicatorValues.Count > 1)
      {
        if ((slowIndicatorValues[0].Value > fastIndicatorValues[0].Value) && (slowIndicatorValues[1].Value < fastIndicatorValues[1].Value))
        {
          //Short position: The fast moving average crossed the slow moving average from above

          Console.WriteLine($@"{slowIndicatorValues[0].Key:yyyy-MM-dd HH:mm:ss} : Short position identified");
        }
        else if ((slowIndicatorValues[0].Value < fastIndicatorValues[0].Value) && (slowIndicatorValues[1].Value > fastIndicatorValues[1].Value))
        {
          //Long position: The fast moving average crossed the slow moving average from below

          Console.WriteLine($@"{slowIndicatorValues[0].Key:yyyy-MM-dd HH:mm:ss} : Long position identified");
        }
      }
    }
  }
}
