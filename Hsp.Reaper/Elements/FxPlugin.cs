namespace Hsp.Reaper.Elements
{

  public class FxPlugin : ElementBase
  {

    public byte[] PluginData { get; private set; }

    public string Name => DefaultProperty.GetString();

    public string Filename => DefaultProperty.GetString(1);

  }

}
