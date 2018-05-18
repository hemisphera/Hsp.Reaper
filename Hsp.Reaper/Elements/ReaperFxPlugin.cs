using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  [ReaperElementNameAttr("VST")]
  public class ReaperFxPlugin : ReaperElement
  {
    internal ReaperFxPlugin(ReaperElement parent)
      : base(parent)
    {
    }

    public byte[] PluginData { get; private set; }
    public string Name
    {
      get { return DefaultProperty[0]; }
    }
    public string Filename
    {
      get { return DefaultProperty[1]; }
    }

    internal override void Parse()
    {
      //string base64Data = String.Concat(Content.Skip(1).TakeWhile(s => !s.StartsWith("<")));
      //PluginData = Convert.FromBase64String(base64Data);
    }
  }

}
