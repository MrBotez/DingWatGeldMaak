using DingWatGeldMaak.FOREX.Data;
using System;
using System.Collections.Generic;

namespace DingWatGeldMaak.FOREX.Markets
{
  public class Chart : IDisposable
  {
    public Dictionary<string, List<object>> Series { get; set; }
    public ChartTypeEnum ChartType { get; private set; }
    public ChartTimeFrameEnum TimeFrame { get; private set; }
    public MarketInformation MarketInfo { get; private set; }

    protected CommodityInformation comodityInfo = null;

    public Chart(CommodityInformation comodityInfo, ChartTypeEnum chartType, ChartTimeFrameEnum chartTimeFrame, IMarketData marketData)
    {
      ChartType = chartType;
      TimeFrame = chartTimeFrame;
      this.comodityInfo = comodityInfo;
    }

    public void Dispose()
    {
      comodityInfo = null;
    }

    public void UpdatePriceData(IEnumerable<OHLC> data)
    {
      foreach (var item in data)
      {
        //Series
      }
    }
  }
}
