using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Reaper.Elements
{

  [ReaperElement("RECORD_CFG")]
  public class RecordConfig : ElementBase
  {

    public byte[] Data { get; set; }

    protected override void ParseContent()
    {
      Data = Convert.FromBase64String(String.Join("", GetContent()));
    }

  }

}
