using DingWatGeldMaak.Core.Providers;
using DingWatGeldMaak.FOREX.Data;
using DingWatGeldMaak.FOREX.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingWatGeldMaak.FOREX.Markets
{
  public class Market : IMarket, IDisposable
  {
    public MarketInformation MarketInfo { get; set; }

    protected Dictionary<string, PriceDataProvider> providers = null;
    protected log4net.ILog logger = null;

    public Market(log4net.ILog logger)
    {
      this.logger = logger;
      MarketInfo = new MarketInformation();
      providers = new Dictionary<string, PriceDataProvider>();
    }

    public void Dispose()
    {
      while (providers.Count > 0)
      {
        string key = providers.First().Key;
        providers[key] = null;

        providers.Remove(key);
      }

      logger = null;
    }

    public void RegisterProvider(string symbol, PriceDataProvider provider)
    {
      if (provider != null)
      {
        if (!providers.ContainsKey(symbol))
        {
          providers.Add(symbol, provider);
        }
      }
    }

    #region IMarket

    public int PlaceOrder(string symbol, double marketPrice, double slippage, double takeProfit, double stopLoss, OrderTypeEnum orderType, DateTime expiresAt)
    {
      throw new NotImplementedException();
    }

    public bool CancelOrder(int orderNumber)
    {
      throw new NotImplementedException();
    }

    public OrderInformation GetOrderInformation(int orderNumber)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<OHLC> GetDataFromDate(string symbol, DateTime fromDate)
    {
      if (providers.ContainsKey(symbol))
      {
        return providers[symbol].GetDataFromDate(fromDate);
      }
      else
      {
        logger?.Warn($@"No provider found for symbol [{(string.IsNullOrEmpty(symbol) ? "" : symbol)}]");

        return new OHLC[0];
      }
    }

    public CommodityInformation GetCommodityInformation(string symbol)
    {
      throw new NotImplementedException();
    }

    #endregion IMarket
  }
}
