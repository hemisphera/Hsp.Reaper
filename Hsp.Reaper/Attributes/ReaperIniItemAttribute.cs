using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsp.Reaper
{

  [AttributeUsage(AttributeTargets.Class)]
  internal class ReaperIniItemAttribute : Attribute
  {

    public string TagName { get; }


    public ReaperIniItemAttribute(string tagName)
    {
      TagName = tagName;
    }

  }

}
