namespace DingWatGeldMaak.FOREX.Markets
{
  public class OrderInformation
  {
    /// <summary>
    /// The order number
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// IS the order still pending
    /// </summary>
    public bool IsPending { get; set; }

    /// <summary>
    /// Is the order closed
    /// </summary>
    public bool IsClosed { get; set; }

    /// <summary>
    /// Is the order currently open
    /// </summary>
    public bool IsOpen { get; set; }

    /// <summary>
    /// The price at which the order was entered
    /// </summary>
    public double EntryPrice { get; set; }

    /// <summary>
    /// The price at which the order was exited
    /// </summary>
    public double ExitPrice { get; set; }

    /// <summary>
    /// The profit of the order in pips
    /// </summary>
    public double PipProfit { get; set; }
  }
}
