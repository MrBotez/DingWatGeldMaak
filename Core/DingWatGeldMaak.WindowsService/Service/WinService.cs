using DingWatGeldMaak.Core.Log;
using DingWatGeldMaak.Core.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DingWatGeldMaak.WindowsService
{
  partial class WinService : ServiceBase
  {
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
    }

    protected void LoadModules()
    {
      // Use the file name to load the assembly into the current
      // application domain.
      Assembly a = Assembly.Load("example");
      // Get the type to use.
      Type myType = a.GetType("Example");
      // Get the method to call.
      MethodInfo myMethod = myType.GetMethod("MethodA");
      // Create an instance.
      object obj = Activator.CreateInstance(myType);
      // Execute the method.
      myMethod.Invoke(obj, null);
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
