using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingWatGeldMaak
{
  public class PriceDataProvider : TimeIntervalDataProvider<int>
  {
    public override void Get()
    {
      var data = new List<int>() { 1, 2, 3, 4, 5 };
      RaiseDataAvailable(data);
    }
  }
}
