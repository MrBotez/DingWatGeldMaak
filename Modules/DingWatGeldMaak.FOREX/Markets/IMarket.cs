using System;

namespace DingWatGeldMaak.FOREX.Markets
{
  public interface IMarket : IMarketData
  {
    /// <summary>
    /// Place an order on the market
    /// </summary>
    /// <param name="symbol">The symbol of the commodity</param>
    /// <param name="marketPrice">The price at which to place the order</param>
    /// <param name="slippage">The slippage in pips to allow when placing the order</param>
    /// <param name="takeProfit">The take profit in pips</param>
    /// <param name="stopLoss">The stop loss in pips</param>
    /// <param name="orderType">The type of order</param>
    /// <param name="expiresAt">The time at which the order must expire if not fulfilled</param>
    /// <returns>The order number</returns>
    int PlaceOrder(string symbol, double marketPrice, double slippage, double takeProfit, double stopLoss, OrderTypeEnum orderType, DateTime expiresAt);

    /// <summary>
    /// Cancel an order by order number
    /// </summary>
    /// <param name="orderNumber">The number of the order to cancel</param>
    /// <returns>The result of the cancel operation</returns>
    bool CancelOrder(int orderNumber);

    /// <summary>
    /// Retrieve order information by order number
    /// </summary>
    /// <param name="orderNumber">The number of the order to get information on</param>
    /// <returns>The order information</returns>
    OrderInformation GetOrderInformation(int orderNumber);

    /// <summary>
    /// Get information on the commodity
    /// </summary>
    /// <param name="symbol">The symbol that identifies the commodity on the market</param>
    /// <returns>The information on the commodity requested</returns>
    CommodityInformation GetCommodityInformation(string symbol);
  }
}
