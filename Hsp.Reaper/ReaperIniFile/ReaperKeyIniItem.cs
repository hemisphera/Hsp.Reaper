using System;

namespace Hsp.Reaper
{

  [ReaperIniItem("KEY")]
  public class ReaperKeyIniItem : IReaperIniItem
  {

    public KeyIniItemShortcutType ShortcutType { get; set; }

    public int Shortcut { get; set; }

    public string Id { get;set; }

    public SectionValidityEnum Validity { get; set; }


    public void Deserialize(string str)
    {
      var parts = str.SplitWithStringDelimiter(' ', '\"');
      ShortcutType = KeyIniItemShortcutType.FromInt(int.Parse(parts[0]));
      Shortcut = int.Parse(parts[1]);
      Id = parts[2];
      Validity = (SectionValidityEnum) int.Parse(parts[3]);
    }

    public string Serialize()
    {
      return String.Join(" ", ShortcutType.ToInt(), Shortcut, Id, (int) Validity);
    }

  }
}