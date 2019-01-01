using System;
using System.Collections.Generic;

namespace DingWatGeldMaak.FOREX.Data
{
  public class DataFrame<T> : IDisposable
  {
    public Dictionary<string, DataSeries<T>> Data { get; set; }
    public DataSeries<T> this[string idx]
    {
      get
      {
        if (!Data.ContainsKey(idx))
        {
          Data.Add(idx, new DataSeries<T>());
        }

        return Data[idx];
      }
      set
      {
        if (!Data.ContainsKey(idx))
        {
          Data.Add(idx, new DataSeries<T>());
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
      Data = new Dictionary<string, DataSeries<T>>();
    }

    public void Dispose()
    {
      
    }
  }
}
