namespace DingWatGeldMaak.FOREX.Indicators
{
  public enum AppliesToEnum
  {
    /// <summary>
    /// The open price
    /// </summary>
    Open,

    /// <summary>
    /// The high price
    /// </summary>
    High,

    /// <summary>
    /// The low price
    /// </summary>
    Low,

    /// <summary>
    /// The close price
    /// </summary>
    Close,

    /// <summary>
    /// The median price (HL / 2)
    /// </summary>
    MedianPrice,

    /// <summary>
    /// Typical price (HLC / 3)
    /// </summary>
    TypicalPrice,

    /// <summary>
    /// Weighted close price (HLCC / 4)
    /// </summary>
    WeightedClosePrice
  }
}
