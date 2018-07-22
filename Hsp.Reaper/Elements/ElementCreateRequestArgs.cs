using System;

namespace Hsp.Reaper.Elements
{

  public class ElementCreateRequestArgs : EventArgs
  {

    public string Name { get; }

    public ElementBase Element { get; set; }


    public ElementCreateRequestArgs(string name)
    {
      Name = name;
    }

  }

}
