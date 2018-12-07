using DingWatGeldMaak.Core.Providers;
using DingWatGeldMaak.FOREX.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingWatGeldMaak.FOREX.Providers
{
  public class PriceDataProvider : TimeIntervalDataProvider<OHLC>
  {
    public override void Get()
    {

    }
  }
}
