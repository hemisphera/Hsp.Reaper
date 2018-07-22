using System;
using System.Collections.Generic;

namespace Hsp.Reaper.Elements
{

  public class MediaItemSource : ElementBase
  {

    public enum ReaperMediaItemType { Midi, Wave, Mp3, Other }

    
    public List<ReaperMidiEvent> Events { get; private set; }

    public MediaItem MediaItem => ParentElement as MediaItem;

    public ReaperMediaItemType ItemType
    {
      get
      {
        var sourceType = DefaultProperty.GetString();
        if (!Enum.TryParse<ReaperMediaItemType>(sourceType, true, out var type))
          type = ReaperMediaItemType.Other;
        return type;
      }
      set => DefaultProperty.SetString(value.ToString().ToUpperInvariant());
    }

    public bool IsExternal => !String.IsNullOrEmpty(Filename);

    public String Filename
    {
      get => GetProperty("FILE").GetString();
      set => GetProperty("FILE").SetString(value);
    }

  }

}
