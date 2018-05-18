namespace Hsp.Reaper
{

  public interface IReaperIniItem
  {

    void Deserialize(string str);

    string Serialize();

  }

}