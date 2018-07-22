using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Hsp.Reaper.Elements;

namespace Hsp.Reaper
{

  public class ElementFactory
  {

    private static ElementFactory _instance;

    public static ElementFactory Instance => _instance ?? (_instance = new ElementFactory());


    public event EventHandler<ElementCreateRequestArgs> ElementCreateRequested;


    public Dictionary<string, Type> ElementTypes { get; }


    private ElementFactory()
    {
      ElementTypes = new Dictionary<string, Type>();

      var items = this.GetType().Assembly.GetTypes()
        .Select(t =>
        {
          var attr = t.GetCustomAttribute<ReaperElementAttribute>();
          if (attr == null) return null;
          return new
          {
            AttributeName = attr.Name,
            Type = t
          };
        }).Where(t => t != null);

      foreach (var item in items)
        ElementTypes.Add(item.AttributeName, item.Type);
    }


    public ElementBase CreateElement(string elementName)
    {
      var args = new ElementCreateRequestArgs(elementName);
      ElementBase element;

      var type = ElementTypes.ContainsKey(elementName) ? ElementTypes[elementName] : null;
      if (type != null)
        element = (ElementBase) Activator.CreateInstance(type);
      else
      {
        ElementCreateRequested?.Invoke(this, args);
        element = args.Element;
      }

      return element ?? new GenericElement();
    }

  }

}
