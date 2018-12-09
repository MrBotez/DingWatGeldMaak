﻿using DingWatGeldMaak.FOREX.Data;
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
    static void Main(string[] args)
    {
      var provider = new HistoryDataProvider(@"C:\DEV\DingWatGeldMaak\HistoryData\DAT_MT_GBPUSD_M1_2017.csv");
      provider.Name = "My provider";
      provider.Interval = TimeSpan.FromSeconds(5);

      provider.OnDataAvailable += Provider_OnDataAvailable;
      provider.Start();


      Console.ReadKey();
      
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
