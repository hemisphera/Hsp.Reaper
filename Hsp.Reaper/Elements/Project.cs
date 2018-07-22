using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  public class ProjectElement : ElementBase
  {

    public IEnumerable<Track> Tracks => ChildElements.OfType<Track>();

    public List<Marker> Markers { get; private set; }

    public List<Region> Regions { get; private set; }


    public string ReaperVersion
    {
      get => DefaultProperty.GetString(1);
      set => DefaultProperty.SetString(value, 1);
    }

    public string Timestamp
    {
      get => DefaultProperty.GetString(2);
      set => DefaultProperty.SetString(value, 2);
    }


    public string MediaPath
    {
      get => GetProperty("RECORD_PATH").GetString();
      set => GetProperty("RECORD_PATH").SetString(value);
    }

    public string MediaSecondaryPath
    {
      get => GetProperty("RECORD_PATH").GetString(1);
      set => GetProperty("RECORD_PATH").SetString(value, 1);
    }

    public bool AutoCrossfade
    {
      get => GetProperty("AUTOXFADE").GetBool();
      set => GetProperty("AUTOXFADE").SetBool(value);
    }

    public bool AllowFeedback
    {
      get => GetProperty("FEEDBACK").GetBool();
      set => GetProperty("FEEDBACK").SetBool(value);
    }

    public bool LoopPlayback
    {
      get => GetProperty("LOOP").GetBool();
      set => GetProperty("LOOP").SetBool(value);
    }

    public bool RippleEditing
    {
      get => GetProperty("RIPPLE").GetBool();
      set => GetProperty("RIPPLE").SetBool(value);
    }

    public double Tempo
    {
      get => GetProperty("TEMPO").GetDouble();
      set => GetProperty("TEMPO").SetDouble(value);
    }

    public string Measure
    {
      get => $"{GetProperty("TEMPO").GetInt(1)}/{GetProperty("TEMPO").GetInt(2)}";
      set
      {
        var parts = value.Split('/');
        GetProperty("TEMPO").SetInt(int.Parse(parts[0]), 1);
        GetProperty("TEMPO").SetInt(int.Parse(parts[1]), 2);
      }
    }

    public IEnumerable<string> Notes
    {
      get
      {
        var noteEl = ChildElements.OfType<ProjectNotes>().FirstOrDefault();
        return noteEl == null ? new string[] { } : noteEl.Text;
      }
      set
      {
        var el = ChildElements.OfType<ProjectNotes>().FirstOrDefault();
        if (el != null && !value.Any())
        {
          ChildElements.Remove(el);
          return;
        }
        if (el == null)
        {
          el = new ProjectNotes();
          ChildElements.Add(el);
        }
        el.Text = value;
      }
    }


    public ProjectElement()
    {
      Markers = new List<Marker>();
      Regions = new List<Region>();
    }

    public ProjectElement(string headerLine)
      : this()
    {
      ParseHeader(headerLine);
    }


    protected override void ParseContent()
    {
      base.ParseContent();

      // parse markers and regions
      var markerProperties = Properties.Where(p => p.PropertyName == "MARKER").ToArray();
      foreach (var markerProperty in markerProperties)
      {
        Properties.Remove(markerProperty);
        if (markerProperty.GetBool(3))
        {
          var marker = new Marker
          {
            Position = markerProperty.GetDouble(1),
            Id = markerProperty.GetInt()
          };
          Markers.Add(marker);
        }
        else
        {
          var regionId = markerProperty.GetInt();
          var pos = markerProperty.GetDouble(1);
          var region = Regions.FirstOrDefault(r => r.Id == regionId);
          if (region == null)
          {
            region = new Region
            {
              Id = regionId,
              Start = pos,
              End = pos
            };
            Regions.Add(region);
          }
          if (String.IsNullOrEmpty(region.Description))
            region.Description = markerProperty.GetString(2);
          if (pos > region.End)
            region.End = pos;
          if (pos < region.Start)
            region.Start = pos;
        }
      }
    }

  }

}
