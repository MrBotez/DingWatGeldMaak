using System;
using System.Linq;
using DingWatGeldMaak.FOREX.Data;

namespace DingWatGeldMaak.FOREX.Indicators
{
  public class MovingAverage : Indicator
  {
    #region Properties

    /// <summary>
    /// The period of the moving average
    /// </summary>
    public int Period { get; set; }

    /// <summary>
    /// The method to use in calculating the moving average aka the type of moving average
    /// </summary>
    public MovingAverageMethodEnum Method { get; set; }

    /// <summary>
    /// What price data must be used in calculating the moving average
    /// </summary>
    public AppliesToEnum AppliesTo { get; set; }

    #endregion Properties

    /// <summary>
    /// Creates a <see cref="MovingAverage"/> object
    /// </summary>
    /// <param name="chartData">The data frame of the chart that this indicator is attached to</param>
    /// <param name="period">The period of the moving average</param>
    /// <param name="method">The method to use in calculating the moving average aka the type of moving average</param>
    /// <param name="appliesTo">What price data must be used in calculating the moving average</param>
    public MovingAverage(DataFrame<double> chartData, int period, MovingAverageMethodEnum method, AppliesToEnum appliesTo) : base(chartData)
    {
      Period = period;
      Method = method;
      AppliesTo = appliesTo;
    }

    /// <summary>
    /// Creates a <see cref="MovingAverage"/> object
    /// </summary>
    /// <param name="chartData">The data frame of the chart that this indicator is attached to</param>
    /// <param name="period">The period of the moving average</param>
    /// <param name="method">The method to use in calculating the moving average aka the type of moving average</param>
    /// <param name="appliesTo">What price data must be used in calculating the moving average</param>
    /// <param name="name">An arbitrary name for the indicator</param>
    public MovingAverage(DataFrame<double> chartData, int period, MovingAverageMethodEnum method, AppliesToEnum appliesTo, string name) : this(chartData, period, method, appliesTo)
    {
      Name = string.IsNullOrEmpty(name) ? "" : name;
    }

    public override void Calculate(DateTime startTime)
    {
      if (this.Method == MovingAverageMethodEnum.Simple)
      {
        CalculateSMA(startTime);
      }
    }

    /// <summary>
    /// Calculate the simple moving average
    /// </summary>
    /// <param name="startTime">The time to start the calculation from</param>
    protected void CalculateSMA(DateTime startTime)
    {
      foreach (var key in Open.Keys.Where(i => i >= startTime))
      {
        switch (AppliesTo)
        {
          case AppliesToEnum.Open:
            {
              var val = Open.GetPreviousDataValues(key, Period).Sum() / Period;

              Data["Buffer"][key] = val;

              break;
            }
          case AppliesToEnum.High:
            {
              var val = High.GetPreviousDataValues(key, Period).Sum() / Period;

              Data["Buffer"][key] = val;

              break;
            }
          case AppliesToEnum.Low:
            {
              var val = Low.GetPreviousDataValues(key, Period).Sum() / Period;

              Data["Buffer"][key] = val;

              break;
            }
          case AppliesToEnum.Close:
            {
              var val = Close.GetPreviousDataValues(key, Period).Sum() / Period;

              Data["Buffer"][key] = val;

              break;
            }
          case AppliesToEnum.MedianPrice:
            {
              var val = High.GetPreviousDataValues(key, Period).Sum();
              val += Low.GetPreviousDataValues(key, Period).Sum();
              val = val / 2;
              val = val / Period;

              Data["Buffer"][key] = val;

              break;
            }
          case AppliesToEnum.TypicalPrice:
            {
              var val = High.GetPreviousDataValues(key, Period).Sum();
              val += Low.GetPreviousDataValues(key, Period).Sum();
              val += Close.GetPreviousDataValues(key, Period).Sum();
              val = val / 3;
              val = val / Period;

              Data["Buffer"][key] = val;

              break;
            }
          case AppliesToEnum.WeightedClosePrice:
            {
              Data["Buffer"][key] = 0;

              break;
            }
        }
      }
    }
  }
}
