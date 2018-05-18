using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  public class ReaperMidiEventsProperty : ReaperProperty
  {
    public List<ReaperMidiEvent> Events { get; private set; }

    public ReaperMidiEventsProperty() :
      base("", "")
    {
      Events = new List<ReaperMidiEvent>();
    }
  }

}
