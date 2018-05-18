using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  public class ReaperMidiEvent
  {
    public int Offset { get; set; }
    public String Status { get; set; }
    public String Data1 { get; set; }
    public String Data2 { get; set; }
    public bool Selected { get; set; }

    public ReaperMidiEvent()
    {
    }
    public ReaperMidiEvent(String str)
    {
      String[] parts = str.Split(' ');
      Selected = parts[0] == "e";
      Offset = int.Parse(parts[1]);
      Status = parts[2];
      Data1 = parts[3];
      Data2 = parts[4];
    }

    public override string ToString()
    {
      return
        String.Format(
          "{0} {1} {2} {3} {4}",
          (Selected ? "e" : "E"),
          Offset, Status, Data1, Data2);
    }
  }

}
