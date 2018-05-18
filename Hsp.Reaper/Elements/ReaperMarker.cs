using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  public class ReaperMarker : ReaperElement
  {
    public int Id { get; set; }
    public double Position { get; set; }

    public ReaperMarker(ReaperProject parentElement)
      : base(parentElement)
    {
    }

    public override string ToString()
    {
      return String.Format("{0}: {1}", Id, Position);
    }
  }

}
