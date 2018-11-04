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
    }

    protected override void OnAfterInstall(IDictionary savedState)
    {
      base.OnAfterInstall(savedState);

      //var sc = new System.ServiceProcess.ServiceController(ServiceConfiguration.Create().ServiceName);
      //sc.Start();
    }

    protected override void OnBeforeInstall(IDictionary savedState)
    {
      base.OnBeforeInstall(savedState);
    }
  }
}
