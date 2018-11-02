using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DingWatGeldMaak.Core.Providers;

namespace DingWatGeldMaak.FOREX.Strategies
{
  public class MovingAverageCrossOverStrategy : Strategy
  {
    public MovingAverageCrossOverStrategy(IDataProvider provider) : base(provider)
    {
    }

    public override void Calculate()
    {
      base.Calculate();
    }

    protected override void Provider_OnDataAvailable(object sender, object data)
    {
      base.Provider_OnDataAvailable(sender, data);
    }
  }
}
