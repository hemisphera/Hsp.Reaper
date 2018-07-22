using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;

namespace Hsp.Reaper.Elements
{

  [ReaperElement("TRACK")]
  public class Track : ElementBase
  {

    public string Name
    {
      get => GetProperty("NAME").GetString();
      set => GetProperty("NAME").SetString(value);
    }

    public Guid Id => Guid.Parse(GetProperty("TRACKID").GetString());

    public Color PeakColor
    {
      get
      {
        var col = GetProperty("PEAKCOL").GetInt();
        if (col == 0)
          col = 16576;
        return Color.FromArgb(col);
      }
      set => GetProperty("PEAKCOL").SetInt(value.ToArgb());
    }

    public bool PhaseInverted
    {
      get => GetProperty("IPHASE").GetBool();
      set => GetProperty("IPHASE").SetBool(value);
    }

    public bool Selected
    {
      get => GetProperty("SEL").GetBool();
      set => GetProperty("SEL").SetBool(value);
    }

    public bool FxEnabled
    {
      get => GetProperty("FX").GetBool();
      set => GetProperty("FX").SetBool(value);
    }

    public TrackTimebase Timebase
    {
      get => (TrackTimebase) GetProperty("BEAT").GetInt();
    }

    public IEnumerable<MediaItem> MediaItems => ChildElements.OfType<MediaItem>();

    public FxChain FxChain => ChildElements.OfType<FxChain>().FirstOrDefault();


    public override string ToString()
    {
      return Name;
    }

  }

}