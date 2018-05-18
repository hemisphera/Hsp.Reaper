using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  [ReaperElementNameAttr("TRACK")]
  public class ReaperTrack : ReaperElement
  {
    public String Name
    {
      get
      {
        return GetProperty("NAME").Value;
      }
    }
    public Guid ID
    {
      get
      {
        return Guid.Parse(GetProperty("TRACKID").Value);
      }
    }
    public Color PeakColor
    {
      get
      {
        ReaperProperty p = GetProperty("PEAKCOL");
        String colorStr = p.Value;
        if (String.IsNullOrEmpty(colorStr))
          colorStr = (16576).ToString();
        return Color.FromArgb(int.Parse(colorStr));
      }
      set
      {
        SetProperty("PEAKCOL", value.ToArgb().ToString());
      }
    }
    public bool PhaseInverted
    {
      get { return GetPropertyValue("IPHASE", 0, "0") == "1"; }
      set { SetProperty("IPHASE", value ? "1" : "0"); }
    }
    public bool Selected
    {
      get { return GetPropertyValue("SEL", 0, "0") == "1"; }
      set { SetProperty("SEL", value ? "1" : "0"); }
    }
    public bool FXEnabled
    {
      get { return GetPropertyValue("FX", 0, "1") == "1"; }
      set { SetProperty("FX", value ? "1" : "0"); }
    }
    public IEnumerable<ReaperMediaItem> MediaItems
    {
      get
      {
        return ChildElements.OfType<ReaperMediaItem>();
      }
    }

    ReaperTrack _ParentTrack;
    public ReaperTrack ParentTrack
    {
      get
      {
        return _ParentTrack;
      }
      internal set
      {
        if (_ParentTrack != null)
          _ParentTrack.ChildTracks.Remove(this);
        _ParentTrack = value;
        if (_ParentTrack != null)
          _ParentTrack.ChildTracks.Add(this);
      }
    }
    public int TrackLevel
    {
      get
      {
        int level = 0;
        ReaperTrack track = this;
        while (track.ParentTrack != null)
        {
          level++;
          track = track.ParentTrack;
        }
        return level;
      }
    }
    public List<ReaperTrack> ChildTracks { get; set; }
    public ReaperFxChain FXChain
    {
      get { return ChildElements.OfType<ReaperFxChain>().FirstOrDefault(); }
    }

    internal ReaperTrack(ReaperElement parent)
      : base(parent)
    {
    }

    public override string ToString()
    {
      return Name;
    }
  }

}
