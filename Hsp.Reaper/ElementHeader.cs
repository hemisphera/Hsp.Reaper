using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hsp.Reaper
{

  internal class ElementHeader
  {

    public string Name { get; }
    
    public string Values { get; }


    public ElementHeader(string line)
    {
      line.AssertItem('<');
      var m = Regex.Match(line, "^\\<(?<name>[A-Z0-9_]+)( (?<props>.*?))?$");
      Name = m.Groups["name"].Value;
      Values = m.Groups["props"].Value;
    }

  }

}
