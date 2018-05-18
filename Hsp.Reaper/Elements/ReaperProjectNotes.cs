using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  [ReaperElementNameAttr("NOTES")]
  public class ReaperProjectNotes : ReaperElement
  {
    public IEnumerable<string> Text { get; set; }

    public ReaperProjectNotes(ReaperElement parent)
      : base(parent)
    {
    }

    internal override void Parse()
    {
      Text = Content.Skip(1).Select(l => l.Length > 0 ? l.Remove(0, 1) : l);
    }
  }

}
