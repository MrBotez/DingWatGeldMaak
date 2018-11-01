using System;

namespace DingWatGeldMaak
{
  public class OHLC
  {
    public OHLC()
    {
      Time = DateTime.MinValue;
      Open = 0.00;
      High = 0.00;
      Low = 0.00;
      Close = 0.00;
      Volume = 0;
    }

    public DateTime Time { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    public int Volume { get; set; }

    public override string ToString()
    {
      return $"Time={Time:yyyy-MM-dd HH:mm:ss} Open={Open:0.00000} High={High:0.00000} Low={Low:0.00000} Close={Close:0.00000} Volume={Volume}";
    }
  }
}
