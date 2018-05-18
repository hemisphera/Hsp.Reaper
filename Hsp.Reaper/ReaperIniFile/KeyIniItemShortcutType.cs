using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Reaper
{

  public class KeyIniItemShortcutType
  {

    public enum TypeEnum
    {
      SingleKey1 = 0,
      SingleKey2 = 1,
      Shift1 = 4,
      Shift2 = 5,
      Ctrl = 9,
      CtrlShift = 13,
      Alt = 17,
      AltShift = 21,
      CtrlAlt = 25,
      CtrlAltMouseWheel = 255,
      Midi = 999,
      Unknown = 9999
    }

    public TypeEnum Type { get; set; }

    public byte MidiChannel { get;set; }
    
    public byte MidiCommand { get;set; }




    public static KeyIniItemShortcutType FromInt(int value)
    {
      value = value % 256;

      var type = Enum.IsDefined(typeof(TypeEnum), value) 
        ? (TypeEnum) value 
        : TypeEnum.Unknown;

      if (type == TypeEnum.Unknown && value < 255 & value > 142)
        type = TypeEnum.Midi;

      var r = new KeyIniItemShortcutType
      {
        Type = type
      };

      if (type == TypeEnum.Midi)
      {
        r.MidiChannel = (byte) (value & 0x0f);
        r.MidiCommand = (byte) (value >> 4);
      }

      return r;
    }


    private KeyIniItemShortcutType()
    {
    }


    public int ToInt()
    {
      if (Type == TypeEnum.Unknown)
        throw new NotSupportedException();
      if (Type != TypeEnum.Midi)
        return (int) Type;
      return (int) (MidiCommand << 4 | MidiChannel);
    }

  }

}
