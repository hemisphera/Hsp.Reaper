using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Hsp.Reaper
{

  public class ReaperKeyboardMap : List<IReaperIniItem>
  {

    internal static readonly Dictionary<string, Type> SupportedIniItems =
      typeof(ReaperKeyboardMap).Assembly.GetTypes()
        .Where(t => t.GetInterfaces().Contains(typeof(IReaperIniItem)))
        .Select(t => new
        {
          Tag = t.GetCustomAttribute<ReaperIniItemAttribute>()?.TagName,
          Type = t
        })
        .Where(at => !String.IsNullOrEmpty(at.Tag))
        .ToDictionary(at => at.Tag, at => at.Type);

    private static string GetDefaultFilename()
    {
      return Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "REAPER",
        "reaper-kb.ini");
    }

    public static ReaperKeyboardMap FromFile(string path = "")
    {
      if (String.IsNullOrEmpty(path))
        path = GetDefaultFilename();

      var r = new ReaperKeyboardMap();

      var lines = File.ReadAllLines(path);
      foreach (var line in lines)
      {
        var m = Regex.Match(line, "^(?<type>[A-Z]{3,}) (?<data>.*)$");
        if (m.Success)
        {
          var type = m.Groups["type"].Value;
          var data = m.Groups["data"].Value;
          r.Add(ParseLine(type, data));
        }
      }

      return r;
    }

    private static IReaperIniItem ParseLine(string tag, string data)
    {
      IReaperIniItem item = null;
      if (!SupportedIniItems.TryGetValue(tag, out var type))
        item = new ReaperUnknownIniItem();
      else
        item = (IReaperIniItem) Activator.CreateInstance(type);
      item.Deserialize(data);
      return item;
    }

    private string[] ToLines()
    {
      return this.Select(i => $"{i.GetTag()} {i.Serialize()}").ToArray();
    }

    public void Save(string path = "")
    {
      if (String.IsNullOrEmpty(path))
        path = GetDefaultFilename();
      File.WriteAllLines(path, ToLines());
    }

  }
}
