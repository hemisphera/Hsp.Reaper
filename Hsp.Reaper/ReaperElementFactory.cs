using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Hsp.Reaper.Elements;

namespace Hsp.Reaper
{

  internal class ReaperElementFactory
  {
    Dictionary<string, Type> elements;

    public ReaperElementFactory()
    {
      elements = new Dictionary<string, Type>();

      this.GetType().Assembly.GetTypes().
      Select(
        t =>
        {
          if (t.IsAbstract || !t.IsSubclassOf(typeof(ReaperElement)))
            return null;
          ReaperElementNameAttr attr = t.GetCustomAttributes(true).OfType<ReaperElementNameAttr>().FirstOrDefault();
          if (attr == null)
            return null;

          return new Tuple<string, Type>(attr.ElementName, t);
        }).
      ToList().
      ForEach(
        itm =>
        {
          if (itm != null)
            elements.Add(itm.Item1, itm.Item2);
        });
    }

    public ReaperElement CreateElement(ReaperElement parent, string elementName)
    {
      if (elementName.StartsWith("<"))
        elementName = String.Concat(elementName.GetWord().Skip(1));

      if (!elements.ContainsKey(elementName))
        return new ReaperElement(parent, elementName);
      else
        return
          (ReaperElement)Activator.CreateInstance(
            elements[elementName],
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
            null,
            new object[] { parent },
            null);
    }

  }
}
