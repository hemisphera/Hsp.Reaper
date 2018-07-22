using System;
using System.Linq;

namespace Hsp.Reaper
{

  [ReaperIniItem("SCR")]
  public class ReaperScriptIniItem : IReaperIniItem
  {

    public SectionValidityEnum Validity { get; set; }

    public int Unknown1 { get; set; }

    public string Id { get; set; }

    public string Description { get; set; }

    public string ScriptPath { get; set; }


    public ReaperScriptIniItem()
    {
      Unknown1 = 4;
    }


    public void Deserialize(string str)
    {
      var parts = str.SplitWithStringDelimiter(' ', '"');
      Unknown1 = int.Parse(parts[0]);
      Validity = (SectionValidityEnum) int.Parse(parts[1]);
      Id = parts[2].Unenclose();
      Description = parts[3].Unenclose();
      ScriptPath = parts[4].Unenclose();
    }

    public string Serialize()
    {
      return String.Join(" ", Unknown1, (int) Validity, Id, Description.Enclose(), ScriptPath.Enclose());
    }

  }

}