using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingWatGeldMaak.FOREX.Configuration
{
  public class ForexModulesConfigurationSection : ConfigurationSection  //ConfigurationElement
  {
    [ConfigurationProperty("name", IsRequired = true)]
    public string Name
    {
      get { return (string)this["name"]; }
      set { this["name"] = value; }
    }

    [ConfigurationProperty("type", IsRequired = true)]
    public string Type
    {
      get { return (string)this["type"]; }
      set { this["type"] = value; }
    }
  }
}
