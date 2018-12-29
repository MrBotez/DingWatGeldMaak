using System;
using System.Collections.Generic;

namespace DingWatGeldMaak.FOREX.Data
{
  public class DataFrame : IDisposable
  {
    public Dictionary<string, DataDictionary<double>> Data { get; set; }
    public DataDictionary<double> this[string idx]
    {
      get
      {
        if (!Data.ContainsKey(idx))
        {
          Data.Add(idx, new DataDictionary<double>());
        }

        return Data[idx];
      }
      set
      {
        if (!Data.ContainsKey(idx))
        {
          Data.Add(idx, new DataDictionary<double>());
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
      Data = new Dictionary<string, DataDictionary<double>>();
    }

    public void Dispose()
    {
      
    }
  }
}
