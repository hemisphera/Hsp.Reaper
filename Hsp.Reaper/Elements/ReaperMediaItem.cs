using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  [ReaperElementNameAttr("ITEM")]
  public class ReaperMediaItem : ReaperElement
  {
    public ReaperMediaItemSource ItemSource
    {
      get { return ChildElements.OfType<ReaperMediaItemSource>().First(); }
    }

    public Guid ID
    {
      get { return Guid.Parse(GetProperty("IGUID").Value); }
    }

    internal ReaperMediaItem(ReaperElement parent)
      : base(parent)
    {
    }

    public override string ToString()
    {
      if (ItemSource != null)
        return String.Format("{0} {1}", ItemSource.ItemType, ItemSource.Filename);
      return base.ToString();
    }
  }

}
