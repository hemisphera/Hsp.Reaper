using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  [ReaperElementNameAttr("FXCHAIN")]
  public class ReaperFxChain : ReaperElement
  {
    public bool Show
    {
      get { return GetPropertyValue("SHOW", 0, "") == "1"; }
      set { SetPropertyValue("SHOW", value ? "1" : "0"); }
    }
    public List<ReaperFx> FX { get; private set; }

    internal ReaperFxChain(ReaperElement parent)
      : base(parent)
    {
    }
  }

}
