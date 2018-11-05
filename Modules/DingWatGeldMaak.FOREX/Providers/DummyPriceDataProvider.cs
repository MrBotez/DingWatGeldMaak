using DingWatGeldMaak.FOREX.Data;
using System;
using System.Collections.Generic;

namespace DingWatGeldMaak.FOREX.Providers
{
  public sealed class DummyPriceDataProvider : PriceDataProvider
  {
    #region Singleton stuff

    private static volatile DummyPriceDataProvider _Instance = null;
    private static readonly object _synclock = new object();

    private DummyPriceDataProvider()
    {
      Interval = TimeSpan.FromSeconds(1);
    }

    public DummyPriceDataProvider Instance
    {
      get
      {
        if (_Instance != null) { return _Instance; }

        lock (_synclock)
        {
          if (_Instance == null)
          {
            _Instance = new DummyPriceDataProvider();
          }
        }

        return _Instance;
      }
    }

    #endregion

    private bool _disposed = false;

    public override void Dispose()
    {
      Dispose(true);

      //base.Dispose();

      GC.SuppressFinalize(true);
    }

    private void Dispose(bool disposing)
    {
      if (_disposed) { return; }

      if (disposing)
      {
        _Instance = null;
      }

      _disposed = true;
    }

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
