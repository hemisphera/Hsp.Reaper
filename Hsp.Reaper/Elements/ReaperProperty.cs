using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  public class ReaperProperty
  {
    List<String> Properties;
    public String PropertyName { get; private set; }
    public bool MultiPart { get { return Properties.Count > 1; } }
    public String Value
    {
      get
      {
        return
          Properties.Count > 0 ?
          Properties[0] :
          "";
      }
    }
    public String this[int index]
    {
      get { return index < Properties.Count ? Properties[index] : ""; }
      set
      {
        if (Properties.Count - 1 < index)
        {
          String[] empty = new String[Properties.Count - 1 - index];
          Properties.AddRange(empty);
        }
        Properties[index] = value;
      }
    }

    public ReaperProperty(String name, String value)
    {
      Properties = new List<String>();
      PropertyName = name;
      String currField = "";
      bool inField = false;
      value = value + ' ';
      foreach (var chr in value)
      {
        if (chr == '"') inField = !inField;
        else if ((chr == ' ') && (!inField))
        {
          Properties.Add(currField);
          currField = "";
        }
        else
        {
          currField = currField + chr;
        }
      }
    }

    public int GetInt(int index)
    {
      return int.Parse(this[index], CultureInfo.InvariantCulture);
    }
    public double GetDouble(int index)
    {
      return double.Parse(this[index], CultureInfo.InvariantCulture);
    }
    public string GetString(int index, bool quoted = true)
    {
      return this[index];
    }

    public override String ToString()
    {
      return
        String.Join(
          " ",
          Properties.Select(
            f =>
            {
              return (f.Contains(' ') || String.IsNullOrEmpty(f)) ? '"' + f + '"' : f;
            }));
    }
  }

}
