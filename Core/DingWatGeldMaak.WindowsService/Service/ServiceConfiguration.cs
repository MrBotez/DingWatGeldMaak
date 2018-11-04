using System;
using System.Configuration;
using System.Reflection;

namespace DingWatGeldMaak.WindowsService
{
  public class ServiceConfiguration
  {
    protected ServiceConfiguration()
    {
      ServiceName = "DingWatGeldMaakWindowsServiceServiceName";
      DisplayName = "DingWatGeldMaak.WindowsService DisplayName";
      Description = "DingWatGeldMaak.WindowsService Description";
    }

    public string ServiceName { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }

    public static ServiceConfiguration Create()
    {
      var rv = new ServiceConfiguration();
      try
      {
        Assembly executingAssembly = Assembly.GetAssembly(typeof(ProjectInstaller));
        string targetDir = executingAssembly.Location;
        Configuration config = ConfigurationManager.OpenExeConfiguration(targetDir);
        rv.ServiceName = config.AppSettings.Settings["Service_ServiceName"].Value.ToString();
        rv.DisplayName = config.AppSettings.Settings["Service_DisplayName"].Value.ToString();
        rv.Description = config.AppSettings.Settings["Service_Description"].Value.ToString();
      }
      catch
      {
      }

      return rv;
    }
  }
}
