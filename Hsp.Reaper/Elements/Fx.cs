using System;
using System.Drawing;

namespace Hsp.Reaper.Elements
{

  public class Fx : ElementBase
  {

    public Guid Id
    {
      get => Guid.Parse(GetProperty("FXID").GetString());
      set => GetProperty("FXID").SetString(value.ToString());
    }

    public bool Bypass
    {
      get => GetProperty("BYPASS").GetBool();
      set => GetProperty("BYPASS").SetBool(value);
    }

    public Rectangle FloatPos => new Rectangle(
      GetProperty("FLOATPOS").GetInt(),
      GetProperty("FLOATPOS").GetInt(1),
      GetProperty("FLOATPOS").GetInt(2),
      GetProperty("FLOATPOS").GetInt(3));

    public FxChain FxChain => ParentElement as FxChain;

  }

}