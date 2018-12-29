using System;

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
    public MovingAverage(DataFrame chartData, int period, MovingAverageMethodEnum method, AppliesToEnum appliesTo) : base(chartData)
    {
      Period = period;
      Method = method;
      AppliesTo = appliesTo;
    }

    public override void Calculate(DateTime startTime)
    {
      
    }
  }
}
