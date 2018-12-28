using DingWatGeldMaak.FOREX.Data;
using DingWatGeldMaak.FOREX.Markets;
using DingWatGeldMaak.FOREX.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingWatGeldMaak.Tests.ConsoleApp
{
  class Program
  {
    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    static void Main(string[] args)
    {
      //var df = new DataFrame();

      //var dt = DateTime.Now;

      //var val = df["Open"][dt];
      //df["Open"][dt] = 12.00;
      //df["Open"][dt] = 13.00;

      //return;

      var provider = new HistoryDataProvider(@"C:\DEV\DingWatGeldMaak\HistoryData\DAT_MT_GBPUSD_M1_2018.csv");
      provider.Name = "My provider";
      provider.Interval = TimeSpan.FromSeconds(5);

      Market market = new Market(logger);
      market.RegisterProvider("GBPUSD", provider);

      var strategy = new Strategy(market);
      strategy.Interval = TimeSpan.FromMilliseconds(0);
      //strategy.DataStartTime = provider.

      var chart = strategy.AddChart("GBPUSD", ChartTypeEnum.OHLC, ChartTimeFrameEnum.M05);
      //chart = strategy.AddChart("GBPUSD", ChartTypeEnum.OHLC, ChartTimeFrameEnum.M15);
      //chart = strategy.AddChart("GBPUSD", ChartTypeEnum.OHLC, ChartTimeFrameEnum.M30);
      //chart = strategy.AddChart("GBPUSD", ChartTypeEnum.OHLC, ChartTimeFrameEnum.H01);

      provider.Start();
      strategy.Start();
      //provider.OnDataAvailable += Provider_OnDataAvailable;

      Console.ReadKey();

      strategy.Stop();
      provider.Stop();
    }

    private static void Provider_OnDataAvailable(object sender, object data)
    {
      var dataReceived = (IEnumerable<OHLC>)data;

      foreach (var item in dataReceived)
      {
        Console.WriteLine($"Data has arrived {item}");
      }
    }
  }
}
