using System;
using System.Collections;
using System.Collections.Generic;

namespace DingWatGeldMaak.FOREX.Data
{
  public class DataDictionary<T> : IDisposable
  {
    public SortedDictionary<DateTime, T> Data { get; set; }

    public T this[DateTime idx]
    {
      get
      {
        if (!Data.ContainsKey(idx))
        {
          Data.Add(idx, default(T));
        }

        return Data[idx];
      }
      set
      {
        if (!Data.ContainsKey(idx))
        {
          Data.Add(idx, default(T));
        }

        Data[idx] = value;
      }
    }

    public IEnumerable<DateTime> Keys { get { return Data.Keys; } }

    public DataDictionary()
    {
      Data = new SortedDictionary<DateTime, T>();
    }

    public bool ContainsKey(DateTime key)
    {
      return Data.ContainsKey(key);
    }

    public void Add(DateTime key, T value)
    {
      Data.Add(key, value);
    }

    public void Dispose()
    {
      Data.Clear();
    }
  }
}
