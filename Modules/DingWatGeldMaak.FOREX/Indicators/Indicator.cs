using DingWatGeldMaak.FOREX.Data;
using System;

namespace DingWatGeldMaak.FOREX.Indicators
{
  public class Indicator : IDisposable
  {
    #region Properties

    /// <summary>
    /// An arbitrary name for the indicator
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Data frame that holds all of the data buffers that the indcator will use
    /// </summary>
    public DataFrame<double> Data { get; set; }

    /// <summary>
    /// Easy access property to the open price data of the chart
    /// </summary>
    public DataSeries<double> Open { get { return chartData["Open"]; } }

    /// <summary>
    /// Easy access property to the high price data of the chart
    /// </summary>
    public DataSeries<double> High { get { return chartData["High"]; } }

    /// <summary>
    /// Easy access property to the low price data of the chart
    /// </summary>
    public DataSeries<double> Low { get { return chartData["Low"]; } }

    /// <summary>
    /// Easy access property to the close price data of the chart
    /// </summary>
    public DataSeries<double> Close { get { return chartData["Close"]; } }

    /// <summary>
    /// Easy access property to the volume data of the chart
    /// </summary>
    public DataSeries<double> Volume { get { return chartData["Volumes"]; } }

    #endregion Properties

    private readonly DataFrame<double> chartData = null;  //Holds the data frame of the chart

    /// <summary>
    /// Creates an <see cref="Indicator"/> object
    /// </summary>
    /// <param name="chartData">The data frame of the chart that this indicator is attached to</param>
    public Indicator(DataFrame<double> chartData)
    {
      Name = Guid.NewGuid().ToString();
      Data = new DataFrame<double>();
      this.chartData = chartData;
    }

    /// <summary>
    /// Creates an <see cref="Indicator"/> object
    /// </summary>
    /// <param name="chartData">The data frame of the chart that this indicator is attached to</param>
    /// <param name="name">An arbitrary name for the indicator</param>
    public Indicator(DataFrame<double> chartData, string name) : this(chartData)
    {
      Name = string.IsNullOrEmpty(name) ? "" : name;
    }

    public virtual void Calculate(DateTime startTime)
    {

    }

    public virtual void Dispose()
    {
    }
  }
}
