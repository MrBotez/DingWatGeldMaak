using System;

namespace DingWatGeldMaak.FOREX.Indicators
{
  public class Indicator : IDisposable
  {
    #region Properties

    /// <summary>
    /// Data frame that holds all of the data buffers that the indcator will use
    /// </summary>
    public DataFrame Data { get; set; }

    /// <summary>
    /// Easy access property to the open price data of the chart
    /// </summary>
    public DataDictionary<double> Open { get { return chartData["Open"]; } }

    /// <summary>
    /// Easy access property to the high price data of the chart
    /// </summary>
    public DataDictionary<double> High { get { return chartData["High"]; } }

    /// <summary>
    /// Easy access property to the low price data of the chart
    /// </summary>
    public DataDictionary<double> Low { get { return chartData["Low"]; } }

    /// <summary>
    /// Easy access property to the close price data of the chart
    /// </summary>
    public DataDictionary<double> Close { get { return chartData["Close"]; } }

    /// <summary>
    /// Easy access property to the volume data of the chart
    /// </summary>
    public DataDictionary<double> Volume { get { return chartData["Volumes"]; } }
    
    #endregion Properties

    private readonly DataFrame chartData = null;  //Holds the data frame of the chart

    /// <summary>
    /// Creates an <see cref="Indicator"/> object
    /// </summary>
    /// <param name="chartData">The data frame of the chart that this indicator is attached to</param>
    public Indicator(DataFrame chartData)
    {
      Data = new DataFrame();
      this.chartData = chartData;
    }

    public virtual void Calculate(DateTime startTime)
    {

    }

    public virtual void Dispose()
    {
    }
  }
}
