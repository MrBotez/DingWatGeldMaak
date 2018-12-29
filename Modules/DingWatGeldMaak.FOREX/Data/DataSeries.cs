using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace DingWatGeldMaak.FOREX.Data
{
  public class DataSeries<T> : IDisposable
  {
    #region Properties

    public SortedList<DateTime, T> DataDescending { get; set; }

    public SortedList<DateTime, T> Data { get; set; }

    public T this[DateTime idx]
    {
      get
      {
        if (!Data.ContainsKey(idx))
        {
          Data.Add(idx, default(T));
          DataDescending.Add(idx, default(T));
        }

        return Data[idx];
      }
      set
      {
        if (!Data.ContainsKey(idx))
        {
          Data.Add(idx, default(T));
          DataDescending.Add(idx, default(T));
        }

        Data[idx] = value;
        DataDescending[idx] = value;
      }
    }

    public IEnumerable<DateTime> Keys { get { return Data.Keys; } }

    #endregion Properties

    public DataSeries()
    {
      Data = new SortedList<DateTime, T>();
      DataDescending = new SortedList<DateTime, T>(new DescendedDateComparer());
    }

    public IEnumerable<T> GetPreviousData(DateTime dateTime, int count)
    {
      var rv = DataDescending.Where(i => i.Key <= dateTime).Take(count);

      return rv.Select(i => i.Value);
    }

    public bool ContainsKey(DateTime key)
    {
      return Data.ContainsKey(key);
    }

    public void Add(DateTime key, T value)
    {
      Data.Add(key, value);
      DataDescending.Add(key, value);
    }

    public void Dispose()
    {
      Data.Clear();
      DataDescending.Clear();
    }
  }
}
