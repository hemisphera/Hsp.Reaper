using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Reaper.Elements;

namespace Hsp.Reaper
{
  class Program
  {
    static void Main(string[] args)
    {
      ReaperProject rpp = ReaperProjectFile.Load(@"C:\Private\Dropbox\UKoG\UKoG Share\Vargorok\Comp.RPP");
      Console.WriteLine(rpp.ReaperVersion);
      foreach (var track in rpp.Tracks)
        Console.WriteLine(track.Name);
      Console.ReadLine();
    }
  }
}
