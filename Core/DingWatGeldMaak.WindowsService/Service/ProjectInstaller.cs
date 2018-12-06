using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DingWatGeldMaak.WindowsService
{
  [RunInstaller(true)]
  public partial class ProjectInstaller : System.Configuration.Install.Installer
  {
    public ProjectInstaller()
    {
      InitializeComponent();

      for (var idx = 0; idx < this.Installers.Count; idx++)
      {
        if (this.Installers[idx] is System.ServiceProcess.ServiceInstaller)
        {
          var cfg = ServiceConfiguration.Create();

          ((System.ServiceProcess.ServiceInstaller)this.Installers[idx]).ServiceName = cfg.ServiceName;
          ((System.ServiceProcess.ServiceInstaller)this.Installers[idx]).DisplayName = cfg.DisplayName;
          ((System.ServiceProcess.ServiceInstaller)this.Installers[idx]).Description = cfg.Description;

          break;
        }
      }
    }

    protected override void OnAfterInstall(IDictionary savedState)
    {
      base.OnAfterInstall(savedState);
    }

    protected override void OnBeforeInstall(IDictionary savedState)
    {
      base.OnBeforeInstall(savedState);
    }
  }
}
