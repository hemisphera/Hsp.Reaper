using System;
using System.Collections.Generic;
using System.Linq;

namespace Hsp.Reaper
{

  [ReaperIniItem("ACT")]
  internal class ReaperActionIniItem : IReaperIniItem
  {

    public SectionValidityEnum Validity { get; set; }
    
    public ConsolidateEnum Consolidate { get; set; }

    public string Id { get; set; }

    public string Name { get; set; }
    
    public List<string> Actions { get; set; }


    public void Deserialize(string str)
    {
      var parts = str.SplitWithStringDelimiter(' ', '\"');
      Consolidate = (ConsolidateEnum) int.Parse(parts[0]);
      Validity = (SectionValidityEnum) int.Parse(parts[0]);
      Id = parts[2].Unenclose();
      Name = parts[3].Unenclose();
      Actions = parts.Skip(4).ToList();
    }

    public string Serialize()
    {
      var line = String.Join(" ", (int)Consolidate, (int)Validity, Id.Enclose(), Name.Enclose());
      if (Actions.Any())
        line = line + " " + String.Join(" ", Actions);
      return line;
    }

  }

}