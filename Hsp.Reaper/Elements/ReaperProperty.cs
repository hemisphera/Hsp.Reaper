using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Hsp.Reaper.Elements
{

  public class ReaperProperty
  {

    public static ReaperProperty FromLine(string line)
    {
      var propertyName = line.GetWord();
      var propertyValue = String.Concat(line.Skip(propertyName.Length + 1));
      return new ReaperProperty(propertyName, propertyValue);
    }


    private List<string> Values { get; }
    

    public string PropertyName { get; }
    
    public bool MultiPart => Values.Count > 1;


    public ReaperProperty(string name, string value)
    {
      Values = new List<string>();
      PropertyName = name;
      ParseValue(value);
    }

    
    public void ParseValue(string value)
    {
      Values.Clear();

      var currValue = "";
      var inField = false;
      value = value + ' ';
      foreach (var chr in value)
      {
        if (chr == '"') 
          inField = !inField;
        else 
        if (chr == ' ' && !inField)
        {
          if (!String.IsNullOrEmpty(currValue))
            Values.Add(currValue);
          currValue = "";
        }
        else
        {
          currValue = currValue + chr;
        }
      }
    }


    public int GetInt(int index = 0)
    {
      return int.Parse(GetString(index), CultureInfo.InvariantCulture);
    }

    public bool GetBool(int index = 0)
    {
      return GetInt(index) == 1;
    }

    public double GetDouble(int index = 0)
    {
      return double.Parse(GetString(index), CultureInfo.InvariantCulture);
    }
    
    public string GetString(int index = 0, bool quoted = true)
    {
      return index < Values.Count ? Values[index] : "";
    }


    public void SetInt(int value, int index = 0)
    {
      SetString($"{value}", index);
    }

    public void SetBool(bool value, int index = 0)
    {
      SetInt(value ? 1 : 0, index);
    }

    public void SetDouble(double value, int index = 0)
    {
      SetString($"{value:F14}", index);
    }
    
    public void SetString(string value, int index = 0, bool quoted = true)
    {
      Values.Fill(index + 1, "");
      Values[index] = value;
    }


    public override string ToString()
    {
      return
        String.Join(
          " ",
          Values.Select(f => f.Contains(' ') || String.IsNullOrEmpty(f) ? '"' + f + '"' : f)
        );
    }

  }

}
