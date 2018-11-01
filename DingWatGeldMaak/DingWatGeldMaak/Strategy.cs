using System.Collections.Generic;

namespace DingWatGeldMaak
{
  public class Strategy : IStrategy
  {
    public IDataProvider DataProvider { get; set; }
    public IEnumerable<ITimeSeries> Buffers { get; set; }

    public Strategy(IDataProvider provider)
    {
      DataProvider = provider;
      provider.OnDataAvailable += Provider_OnDataAvailable;

      Buffers = new List<ITimeSeries>();
    }

    protected virtual void Provider_OnDataAvailable(object sender, object data)
    {
      //Prepare the buffers


      Calculate();
    }

    public virtual void Calculate()
    {

    }
  }
}
