using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  public class ReaperRegion : ReaperElement
  {
    public int Id { get; set; }
    public double Start { get; set; }
    public double End { get; set; }
    public string Description { get; set; }

    public ReaperRegion(ReaperProject parentElement)
      : base(parentElement)
    {
    }

    public override string ToString()
    {
      return String.Format("{0}: {1}", Id, Description);
    }
  }

}
