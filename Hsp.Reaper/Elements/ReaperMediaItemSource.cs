using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  [ReaperElementNameAttr("SOURCE")]
  public class ReaperMediaItemSource : ReaperElement
  {
    public enum ReaperMediaItemType { MIDI, Wave, MP3, Other }
    public List<ReaperMidiEvent> Events { get; private set; }

    public ReaperMediaItem MediaItem
    {
      get
      {
        return ParentElement as ReaperMediaItem;
      }
    }
    public ReaperMediaItemType ItemType
    {
      get
      {
        String sourceType = DefaultProperty[1];
        ReaperMediaItemType it = ReaperMediaItemType.Other;
        Enum.TryParse<ReaperMediaItemType>(sourceType, true, out it);
        return it;
      }
      set
      {
        //DefaultProperty.Value = value.ToString().ToUpper();
        Events.Clear();
      }
    }
    public bool IsExternal
    {
      get { return !String.IsNullOrEmpty(Filename); }
    }
    public String Filename
    {
      get
      {
        ReaperProperty file = GetProperty("FILE");
        return
          file != null ?
          file[0] :
          "";
      }
    }

    internal ReaperMediaItemSource(ReaperElement parent)
      : base(parent)
    {
    }

  }

}
