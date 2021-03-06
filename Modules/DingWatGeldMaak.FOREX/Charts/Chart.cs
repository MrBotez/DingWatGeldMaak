﻿using System;
using System.Linq;
using System.Collections.Generic;
using DingWatGeldMaak.FOREX.Data;
using DingWatGeldMaak.FOREX.Markets;
using DingWatGeldMaak.FOREX.Indicators;
using System.Threading.Tasks;

namespace DingWatGeldMaak.FOREX.Charts
{
  public class Chart : IDisposable
  {
    public string Name { get; set; }
    public DataFrame<double> ChartData { get; set; }
    public ChartTypeEnum ChartType { get; private set; }
    public ChartTimeFrameEnum TimeFrame { get; private set; }
    public MarketInformation MarketInfo { get; private set; }

    protected CommodityInformation comodityInfo = null;
    protected DataSeries<OHLC> dataReceived = null;
    protected List<Indicator> indicators = null;

    public Chart(CommodityInformation comodityInfo, ChartTypeEnum chartType, ChartTimeFrameEnum chartTimeFrame, IMarketData marketData)
    {
      Name = Guid.NewGuid().ToString();
      ChartType = chartType;
      TimeFrame = chartTimeFrame;
      this.comodityInfo = comodityInfo;
      ChartData = new DataFrame<double>();
      dataReceived = new DataSeries<OHLC>();
      indicators = new List<Indicator>();
    }

    public Chart(CommodityInformation comodityInfo, ChartTypeEnum chartType, ChartTimeFrameEnum chartTimeFrame, IMarketData marketData, string name)
      : this(comodityInfo, chartType, chartTimeFrame, marketData)
    {
      Name = name;
    }

    public void Dispose()
    {
      comodityInfo = null;
      dataReceived.Dispose();
    }

    protected DateTime ConvertToTimeframe(DateTime value)
    {
      switch (TimeFrame)
      {
        case ChartTimeFrameEnum.M05:
          {
            var minutes = value.Minute / 5 * 5;
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, minutes, 0);
          }
        case ChartTimeFrameEnum.M10:
          {
            var minutes = value.Minute / 10 * 10;
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, minutes, 0);
          }
        case ChartTimeFrameEnum.M15:
          {
            var minutes = value.Minute / 15 * 15;
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, minutes, 0);
          }
        case ChartTimeFrameEnum.M20:
          {
            var minutes = value.Minute / 20 * 20;
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, minutes, 0);
          }
        case ChartTimeFrameEnum.M30:
          {
            var minutes = value.Minute / 30 * 30;
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, minutes, 0);
          }
        case ChartTimeFrameEnum.H01:
          {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, 0, 0);
          }
        default: { return value; }
      }
    }

    public void AddIndicator(Indicator indicator)
    {
      if (indicator != null)
      {
        indicators.Add(indicator);
      }
    }

    public Indicator GetIndicatorByName(string name)
    {
      return indicators.FirstOrDefault(i => i.Name == name);
    }

    public void UpdatePriceData(IEnumerable<OHLC> data)
    {
      foreach (var item in data)
      {
        dataReceived[item.Time] = item;
      }

      var dataLow = ChartData["Low"];        //
      var dataHigh = ChartData["High"];      // Saving a few cycles on dictionary access
      var dataOpen = ChartData["Open"];      // throughout the rest of the method
      var dataClose = ChartData["Close"];    //
      var dataVolume = ChartData["Volume"];  //

      var startTime = data.Select(i => i.Time).Min();
      startTime = ConvertToTimeframe(startTime);
      var lastTime = DateTime.MinValue;

      foreach (var key in dataReceived.Keys.Where(i => i >= startTime))
      {
        var item = dataReceived[key];
        var timeKey = ConvertToTimeframe(item.Time);

        if (lastTime != timeKey)
        {
          dataOpen[timeKey] = item.Open;
          dataHigh[timeKey] = double.MinValue;
          dataLow[timeKey] = double.MaxValue;
          dataClose[timeKey] = 0;
          dataVolume[timeKey] = 0;

          lastTime = timeKey;
        }

        var low = dataLow[timeKey];
        var high = dataHigh[timeKey];

        dataHigh[timeKey] = high < item.High ? item.High : high;
        dataLow[timeKey] = low > item.Low ? item.Low : low;
        dataClose[timeKey] = item.Close;
        dataVolume[timeKey] += item.Volume;
      }

      Parallel.ForEach(indicators, (indicator) => { indicator.Calculate(startTime); });
    }
  }
}
