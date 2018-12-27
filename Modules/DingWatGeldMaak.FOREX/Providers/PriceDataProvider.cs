using System;
using DingWatGeldMaak.Core.Providers;
using DingWatGeldMaak.FOREX.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingWatGeldMaak.FOREX.Providers
{
  public class PriceDataProvider : TimeIntervalDataProvider<OHLC>
  {
    protected List<OHLC> historyData = null;
    protected SortedList<DateTime, OHLC> dataIndex = null;

    public PriceDataProvider() : base()
    {
      historyData = new List<OHLC>();
      dataIndex = new SortedList<DateTime, OHLC>();
    }

    public override void Dispose()
    {
      historyData.Clear();
      dataIndex.Clear();

      base.Dispose();
    }

    public override void Get()
    {

    }

    public virtual List<OHLC> Get(int candleCount)
    {

      return historyData.Skip(historyData.Count - candleCount).ToList();
    }
  }
}
