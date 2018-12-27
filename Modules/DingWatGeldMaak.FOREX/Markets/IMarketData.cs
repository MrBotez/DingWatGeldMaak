using DingWatGeldMaak.FOREX.Data;
using System;
using System.Collections.Generic;

namespace DingWatGeldMaak.FOREX.Markets
{
  public interface IMarketData
  {
    /// <summary>
    /// Retrieves data for a commodity from the market
    /// </summary>
    /// <param name="symbol">The symbol of the commodity</param>
    /// <param name="fromDate">The date from which onwards to return data</param>
    /// <returns>The collection of data requested</returns>
    IEnumerable<OHLC> GetDataFromDate(string symbol, DateTime fromDate);
  }
}
