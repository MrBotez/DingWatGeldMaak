using System;
using System.Collections.Generic;

namespace DingWatGeldMaak
{
  public class DummyPriceDataProvider : PriceDataProvider
  {
    public override void Get()
    {
      var data = new List<OHLC>() {

        new OHLC().SetTime(DateTime.Now.AddMinutes(-9)).SetVolume(9),
        new OHLC().SetTime(DateTime.Now.AddMinutes(-8)).SetVolume(8),
        new OHLC().SetTime(DateTime.Now.AddMinutes(-7)).SetVolume(7),
        new OHLC().SetTime(DateTime.Now.AddMinutes(-6)).SetVolume(6),
        new OHLC().SetTime(DateTime.Now.AddMinutes(-5)).SetVolume(5),
        new OHLC().SetTime(DateTime.Now.AddMinutes(-4)).SetVolume(4),
        new OHLC().SetTime(DateTime.Now.AddMinutes(-3)).SetVolume(3),
        new OHLC().SetTime(DateTime.Now.AddMinutes(-2)).SetVolume(2),
        new OHLC().SetTime(DateTime.Now.AddMinutes(-1)).SetVolume(1)

      };

      RaiseDataAvailable(data);
    }
  }
}
