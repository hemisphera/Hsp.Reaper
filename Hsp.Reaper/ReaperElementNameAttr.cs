using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsp.Reaper
{

  [AttributeUsage(AttributeTargets.Class)]
  internal class ReaperElementNameAttr : Attribute
  {
    public string ElementName { get; set; }
    public ReaperElementNameAttr(string elementName)
    {
      ElementName = elementName;
    }
  }

}
