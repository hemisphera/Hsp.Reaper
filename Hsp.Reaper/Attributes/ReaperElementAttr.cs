using System;

namespace Hsp.Reaper
{

  [AttributeUsage(AttributeTargets.Class)]
  public class ReaperElementAttribute : Attribute
  {

    public string Name { get; set; }
    
    public ReaperElementAttribute(string name)
    {
      Name = name;
    }

  }

}
