using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingWatGeldMaak.FOREX
{
  public class ForexModule : Core.Modules.Module
  {
    //public DataProvider

    public ForexModule() : base()
    {
      _Id = Guid.Parse("40C11D6C-4FC4-4D05-ADD5-96EE00D4E42A");

      _Name = "ForexModule";
    }

    public override void Start()
    {
      base.Start();
    }

    public override void Stop()
    {
      base.Stop();
    }
  }
}
