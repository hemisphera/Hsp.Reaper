using System;

namespace Hsp.Reaper.Elements
{

  [ReaperElement("RENDER_CFG")]
  public class RenderConfig : ElementBase
  {

    public byte[] Data { get; set; }

    protected override void ParseContent()
    {
      Data = Convert.FromBase64String(String.Join("", GetContent()));
    }

  }

}
