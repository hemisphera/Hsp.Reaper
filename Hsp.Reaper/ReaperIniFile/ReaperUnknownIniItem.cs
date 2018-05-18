namespace Hsp.Reaper
{

  internal class ReaperUnknownIniItem : IReaperIniItem
  {

    public string Data { get; private set; }

    public void Deserialize(string str)
    {
      Data = str;
    }

    public string Serialize()
    {
      return Data;
    }

  }

}