using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingWatGeldMaak.Core.Modules
{
  public interface IModule
  {
    Guid Id { get; }
    string Name { get; }
    log4net.ILog Logger { get; }

    void Initialize(log4net.ILog logger);
    void Start();
    void Stop();
  }
}
