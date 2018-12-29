using System;
using System.Collections.Generic;

namespace DingWatGeldMaak.FOREX.Data
{
  public class DataFrame : IDisposable
  {
    public Dictionary<string, DataSeries<double>> Data { get; set; }
    public DataSeries<double> this[string idx]
    {
      get
      {
        if (!Data.ContainsKey(idx))
        {
          Data.Add(idx, new DataSeries<double>());
        }

        return Data[idx];
      }
      set
      {
        if (!Data.ContainsKey(idx))
        {
          Data.Add(idx, new DataSeries<double>());
        }

        if (value != null)
        {
          var item = Data[idx];

          foreach (var key in value.Keys)
          {
            if (item.ContainsKey(key))
            {
              item[key] = value[key];
            }
            else
            {
              item.Add(key, value[key]);
            }
          }
        }
        else
        {
          Data[idx] = null;
          Data.Remove(idx);
        }
      }
    }

    public DataFrame()
    {
      Data = new Dictionary<string, DataSeries<double>>();
    }

    public void Dispose()
    {
      
    }
  }
}
