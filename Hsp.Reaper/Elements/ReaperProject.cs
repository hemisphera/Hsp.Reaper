using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  [ReaperElementNameAttr("REAPER_PROJECT")]
  public class ReaperProject : ReaperElement
  {
    public IEnumerable<ReaperTrack> Tracks
    {
      get
      {
        return ChildElements.OfType<ReaperTrack>();
      }
    }
    public IEnumerable<ReaperTrack> TopLevelTracks
    {
      get
      {
        return Tracks.Where(t => { return t.ParentTrack == null; });
      }
    }
    public List<ReaperMarker> Markers { get; private set; }
    public List<ReaperRegion> Regions { get; private set; }
    public String ReaperVersion
    {
      get { return DefaultProperty[2]; }
    }
    public String MediaPath
    {
      get { return GetPropertyValue("RECORD_PATH", 0, ""); }
      set { SetPropertyValue("RECORD_PATH", value); }
    }
    public String MediaSecondaryPath
    {
      get { return GetPropertyValue("RECORD_PATH", 1, ""); }
      set { SetPropertyValue("RECORD_PATH", value, 1); }
    }
    public bool AutoCrossfade
    {
      get { return GetPropertyValue("AUTOXFADE", 0, "") == "1"; }
      set { SetPropertyValue("AUTOXFADE", value ? "1" : "0"); }
    }
    public bool AllowFeedback
    {
      get { return GetPropertyValue("FEEDBACK", 0, "") == "1"; }
      set { SetPropertyValue("FEEDBACK", value ? "1" : "0", 0); }
    }
    public bool LoopPlayback
    {
      get { return GetPropertyValue("LOOP", 0, "") == "1"; }
      set { SetPropertyValue("LOOP", value ? "1" : "0", 0); }
    }
    public bool RippleEditing { get { return GetProperty("RIPPLE").Value == "1"; } }
    public int Tempo
    {
      get { return int.Parse(GetPropertyValue("TEMPO", 0, "0")); }
      set { SetPropertyValue("TEMPO", value.ToString(), 0); }
    }
    public String Measure
    {
      get
      {
        return
          String.Format(
            "{0}/{1}",
            GetPropertyValue("TEMPO", 1, "4"),
            GetPropertyValue("TEMPO", 2, "4")
            );
      }
      set
      {
        String[] parts = value.Split('/');
        SetPropertyValue("TEMPO", parts[0], 1);
        SetPropertyValue("TEMPO", parts[1], 2);
      }
    }
    public IEnumerable<string> Notes
    {
      get
      {
        ReaperProjectNotes noteEl = ChildElements.OfType<ReaperProjectNotes>().FirstOrDefault();
        if (noteEl == null)
          return new string[] { };
        return noteEl.Text;
      }
      set
      {
        ReaperProjectNotes noteEl = ChildElements.OfType<ReaperProjectNotes>().FirstOrDefault();
        if ((noteEl != null) && (value.Count() == 0))
        {
          ChildElements.Remove(noteEl);
          return;
        }
        if (noteEl == null)
        {
          noteEl = new ReaperProjectNotes(this);
          ChildElements.Add(noteEl);
        }
        noteEl.Text = value;
      }
    }
    public Guid ProjectId
    {
      get
      {
        string[] notes = Notes.ToArray();
        Guid projId = Guid.Empty;
        bool f = false;
        if (notes.Length > 0)
          f = Guid.TryParse(notes[0], out projId);
        return projId;
      }
      set
      {
        List<string> notes = Notes.ToList();
        Guid guid = Guid.Empty;
        if (notes.Count == 0)
        {
          notes.Add(value.ToString());
          return;
        }
        if (Guid.TryParse(notes[0], out guid))
          notes[0] = value.ToString();
        else
          notes.Insert(0, value.ToString());
      }
    }

    internal ReaperProject(ReaperElement parent)
      : base(null)
    {
      Markers = new List<ReaperMarker>();
      Regions = new List<ReaperRegion>();
    }

    internal override void Parse()
    {
      base.Parse();

      // parse markers and regions
      var markerProperties = Properties.Where(p => p.PropertyName == "MARKER").ToList();
      foreach (ReaperProperty markerProperty in markerProperties)
      {
        Properties.Remove(markerProperty);
        if (markerProperty[3] == "0")
        {
          ReaperMarker marker = new ReaperMarker(this);
          marker.Position = markerProperty.GetDouble(1);
          marker.Id = markerProperty.GetInt(0);
          Markers.Add(marker);
        }
        else
        {
          int regionId = markerProperty.GetInt(0);
          double pos = markerProperty.GetDouble(1);
          ReaperRegion region = Regions.FirstOrDefault(r => r.Id == regionId);
          if (region == null)
          {
            region = new ReaperRegion(this);
            region.Id = regionId;
            region.Start = pos;
            region.End = pos;
            Regions.Add(region);
          }
          if (String.IsNullOrEmpty(region.Description))
            region.Description = markerProperty[2];
          if (pos > region.End)
            region.End = pos;
          if (pos < region.Start)
            region.Start = pos;
        }
      }
    }

  }

}
