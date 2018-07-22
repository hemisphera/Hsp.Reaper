using System.Collections.Generic;
using System.Linq;

namespace Hsp.Reaper.Elements
{

  public class ProjectNotes : ElementBase
  {
    
    public IEnumerable<string> Text { get; set; }

    protected override void ParseContent()
    {
      Text = GetContent()
        .Skip(1)
        .Select(l => l.Length > 0 ? l.Remove(0, 1) : l);
    }

  }

}
