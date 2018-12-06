using DingWatGeldMaak.Core.Log;
using DingWatGeldMaak.Core.Modules;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DingWatGeldMaak.WindowsService
{
  partial class WinService : ServiceBase
  {
    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public WinService()
    {
      InitializeComponent();

      modules = new List<IModule>();

      EventLogger.Instance.EventLogSource = this.ServiceName;

    }

    private IList<IModule> modules = null;

    protected void UnloadModules()
    {
      Parallel.ForEach(modules, (module) => { module.Stop(); });

      //while (modules.Count > 0)
      //{
      //  IModule module = modules[0];
      //}

      modules.Clear();
    }

    protected void LoadModules()
    {
      var fi = new FileInfo(Assembly.GetExecutingAssembly().Location);

      foreach(var dll in fi.Directory.GetFiles("*.dll"))
      {
        var asm = Assembly.LoadFrom(dll.FullName);
        var modulesFound = asm.GetExportedTypes().Where(i => i.GetInterface("IModule") == typeof(IModule));

        foreach(var mod in modulesFound)
        {
          IModule obj = (IModule)Activator.CreateInstance(mod.UnderlyingSystemType);
          obj.Initialize(logger);
          modules.Add(obj);
        }
      }

      Parallel.ForEach(modules, (mod) => { mod.Start(); });
    }

    protected override void OnStart(string[] args)
    {
      EventLogger.Instance.LogToEventLog("Starting the service.", LogType.ltInfo);

      try
      {
        if (Environment.UserInteractive)
        {

        }

        LoadModules();

        //Data.DataContext.Instance.Initialize();

        //if (webService != null)
        //{
        //  webService.Dispose();
        //}
        //else
        //{
        //  string baseAddress = ConfigurationManager.AppSettings["baseAddress"];

        //  // Start OWIN host 
        //  webService = WebApp.Start<WebAPI.Startup>(url: baseAddress);
        //}

        EventLogger.Instance.LogToEventLog("The service has started.", LogType.ltInfo);
      }
      catch (Exception ex)
      {
        EventLogger.Instance.LogToEventLog(String.Format("The service failed to start. {0}", ex.ToString()), LogType.ltError);
      }
    }

    protected override void OnStop()
    {
      EventLogger.Instance.LogToEventLog("Stopping the service.", LogType.ltInfo);

      try
      {
        UnloadModules();

        //webService?.Dispose();

        EventLogger.Instance.LogToEventLog("The service has stopped.", LogType.ltInfo);
      }
      catch (Exception ex)
      {
        EventLogger.Instance.LogToEventLog(String.Format("The service failed to stop. {0}", ex.ToString()), LogType.ltError);
      }
    }
  }
}
