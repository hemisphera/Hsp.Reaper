using System;
using System.Linq;

namespace Hsp.Reaper.Elements
{

  [ReaperElement("ITEM")]
  public class MediaItem : ElementBase
  {

    public MediaItemSource ItemSource => ChildElements.OfType<MediaItemSource>().Single();

    public string Name
    {
      get => GetProperty("NAME").GetString();
      set => GetProperty("NAME").SetString(value);
    }

    public double Position
    {
      get => GetProperty("POSITION").GetDouble();
      set => GetProperty("POSITION").SetDouble(value);
    }

    public double SnapOffset
    {
      get => GetProperty("SNAPOFFS").GetDouble();
      set => GetProperty("SNAPOFFS").SetDouble(value);
    }

    public double Length
    {
      get => GetProperty("LENGTH").GetDouble();
      set => GetProperty("LENGTH").SetDouble(value);
    }

    public bool Muted
    {
      get => GetProperty("MUTE").GetBool();
      set => GetProperty("MUTE").SetBool(value);
    }

    public Guid Id
    {
      get => Guid.Parse(GetProperty("IGUID").GetString());
      set => GetProperty("IGUID").SetString(value.ToString());
    }


    public override string ToString()
    {
      return ItemSource != null ? $"{ItemSource.ItemType} {ItemSource.Filename}" : base.ToString();
    }

  }

}
