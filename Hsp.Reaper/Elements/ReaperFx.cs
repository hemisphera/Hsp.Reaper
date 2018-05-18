using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  [ReaperElementNameAttr("FX")]
  public class ReaperFx : ReaperElement
  {
    public Guid ID
    {
      get { return Guid.Parse(GetPropertyValue("FXID", 0, "")); }
      set { SetPropertyValue("FXID", value.ToString(), 0); }
    }
    public bool Bypass
    {
      get { return GetPropertyValue("BYPASS", 0, "0") == "1"; }
      set { SetPropertyValue("BYPASS", value ? "1" : "0", 0); }
    }
    public Rectangle FloatPos
    {
      get
      {
        return new Rectangle(
          int.Parse(GetPropertyValue("FLOATPOS", 0, "0")),
          int.Parse(GetPropertyValue("FLOATPOS", 1, "0")),
          int.Parse(GetPropertyValue("FLOATPOS", 2, "0")),
          int.Parse(GetPropertyValue("FLOATPOS", 3, "0")));
      }
    }

    ReaperFxChain _FXChain;
    public ReaperFxChain FXChain
    {
      get { return _FXChain; }
      set
      {
        if (FXChain != null)
          FXChain.FX.Remove(this);
        _FXChain = value;
        if (FXChain != null)
          FXChain.FX.Add(this);
      }
    }

    internal ReaperFx(ReaperElement parent)
      : base(parent)
    {
    }
  }

}
