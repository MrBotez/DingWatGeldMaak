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

    /// <summary>
    /// Return a number of historic data points from a point in time backwards indexed by time
    /// </summary>
    /// <param name="dateTime">The start time at which the data range should start from</param>
    /// <param name="count">The number of data points into the past</param>
    /// <returns>The values indexed by their times of the data points found</returns>
    public IEnumerable<KeyValuePair<DateTime, T>> GetPreviousData(DateTime dateTime, int count)
    {
      var rv = DataDescending.Where(i => i.Key <= dateTime).Take(count);

      return rv;
    }

    /// <summary>
    /// Return a number of historic data point values from a point in time backwards
    /// </summary>
    /// <param name="dateTime">The start time at which the data range should start from</param>
    /// <param name="count">The number of data points into the past</param>
    /// <returns>The values only of the data points found</returns>
    public IEnumerable<T> GetPreviousDataValues(DateTime dateTime, int count)
    {
      var rv = GetPreviousData(dateTime, count);

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
