using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Reaper
{

  internal static class Extensions
  {

    public static string GetWord(this IEnumerable<char> str)
    {
      var newString = string.Concat(str).Trim();
      return String.Concat(newString.TakeWhile(f => f != ' '));
    }

    public static string SkipWord(this string line)
    {
      var word = line.GetWord();
      return String.Concat(line.Skip(word.Length + 1));
    }


    public static string Unenclose(this string str, string delimiter = "\"")
    {
      if (str.StartsWith(delimiter) && str.EndsWith(delimiter))
        return str.Substring(delimiter.Length, str.Length - delimiter.Length * 2);
      return str;
    }

    public static string Enclose(this string str, string delimiter = "\"")
    {
      return $"{delimiter}{str}{delimiter}";
    }

    public static string GetTag(this IReaperIniItem item)
    {
      var type = item.GetType();
      var entry = ReaperKeyboardMap.SupportedIniItems.FirstOrDefault(kvp => kvp.Value == type);
      return entry.Key ?? "";
    }

    public static string[] SplitWithStringDelimiter(this string line, char delimiter, char stringDelimiter)
    {
      var isInString = false;
      var currPart = "";
      var parts = new List<string>();

      foreach (var chr in line)
      {
        if (chr.Equals(stringDelimiter))
          isInString = !isInString;

        if (chr.Equals(delimiter) && !isInString)
        {
          parts.Add(currPart);
          currPart = "";
        }

        if (chr != delimiter || isInString)
          currPart += chr;
      }
      if (!String.IsNullOrEmpty(currPart))
        parts.Add(currPart);

      return parts.ToArray();
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
      foreach (var item in source)
      {
        action(item);
        yield return item;
      }
    }


    public static void AssertItem<T>(this IEnumerable<T> items, T item)
    {
      if (!items.First().Equals(item))
        throw new FormatException($"The first item must be '{item}'.");
    }

    public static void Fill<T>(this IList<T> list, int length, T item)
    {
      while (list.Count < length)
        list.Add(item);
    }

  }

}
