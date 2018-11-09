using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Core;

namespace DingWatGeldMaak.Core.Modules
{
  public class Module : IModule
  {
    public Module()
    {
      _Id = Guid.NewGuid();
      _Name = "Module";
      _Logger = null;
    }

    protected log4net.ILog _Logger = null;
    public log4net.ILog Logger { get { return _Logger; } }

    protected Guid _Id;
    public Guid Id { get { return _Id; } }

    protected string _Name;
    public string Name { get { return _Name; } }

    public virtual void Start()
    {
      Logger?.Info($"Starting module {Name}");
    }

    public virtual void Stop()
    {
      Logger?.Info($"Stopping module {Name}");
    }

    public virtual void Initialize(ILog logger)
    {
      _Logger = logger;
    }
  }
}
