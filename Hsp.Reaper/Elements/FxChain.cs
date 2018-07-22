using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  [ReaperElement("FXCHAIN")]
  public class FxChain : ElementBase
  {

    public bool Show
    {
      get => GetProperty("SHOW").GetBool();
      set => GetProperty("SHOW").SetBool(value);
    }

    public IEnumerable<Fx> Fx => ChildElements.OfType<Fx>();

  }

}
