using System;

namespace DingWatGeldMaak
{
  public static class OHLCExtension
  {
    public static OHLC SetTime(this OHLC self, DateTime value)
    {
      if (self != null) { self.Time = value; }

      return self;
    }

    public static OHLC SetOpen(this OHLC self, double value)
    {
      if (self != null) { self.Open = value; }

      return self;
    }

    public static OHLC SetHigh(this OHLC self, double value)
    {
      if (self != null) { self.High = value; }

      return self;
    }

    public static OHLC SetLow(this OHLC self, double value)
    {
      if (self != null) { self.Low = value; }

      return self;
    }

    public static OHLC SetClose(this OHLC self, double value)
    {
      if (self != null) { self.Close = value; }

      return self;
    }

    public static OHLC SetVolume(this OHLC self, int value)
    {
      if (self != null) { self.Volume = value; }

      return self;
    }
  }
}
