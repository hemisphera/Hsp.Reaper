namespace Hsp.Reaper.Elements
{

  public sealed class ReaperDefaultProperty : ReaperProperty
  {

    public const string Key = "_DEFAULT_";

    public ReaperDefaultProperty(string value)
      : base(Key, value)
    {
    }

  }

}
