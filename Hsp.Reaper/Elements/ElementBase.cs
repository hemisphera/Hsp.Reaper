using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Hsp.Reaper.Elements
{

  public abstract class ElementBase
  {

    protected List<ReaperProperty> Properties { get; set; }
    
    private StringBuilder Source { get; set; }
    
    public string ElementName { get; private set; }

    public ElementBase ParentElement { get; private set; }
    
    public ObservableCollection<ElementBase> ChildElements;
    
    public IEnumerable<string> PropertyNames
    {
      get
      {
        return Properties.Select(p => p.PropertyName).Where(p => p != "_DEFAULT_");
      }
    }


    public ReaperDefaultProperty DefaultProperty { get; }


    protected ElementBase()
    {
      Source = new StringBuilder();
      Properties = new List<ReaperProperty>();
      DefaultProperty = new ReaperDefaultProperty("");

      ChildElements = new ObservableCollection<ElementBase>();
      ChildElements.CollectionChanged += (s, e) =>
      {
        if (e.OldItems != null)
          foreach (var oldItem in e.OldItems.Cast<ElementBase>())
            oldItem.ParentElement = null;
        if (e.NewItems != null)
          foreach (var newItem in e.NewItems.Cast<ElementBase>())
            newItem.ParentElement = this;
      };
    }


    public virtual void Read(TextReader reader)
    {
      string line;
      do
      {
        line = reader.ReadLine()?.TrimStart();

        if (IsStartOfSubElement(line))
        {
          var header = new ElementHeader(line);
          var childElement = ElementFactory.Instance.CreateElement(header.Name);
          ChildElements.Add(childElement);
          childElement.ParseHeader(line);
          childElement.Read(reader);
        }
        else
          Source.AppendLine(line);
      } while (!IsEndOfElement(line));

      ParseContent();
    }

    public virtual void Write(TextWriter writer)
    {
    }


    protected virtual bool IsStartOfSubElement(string line)
    {
      return line.StartsWith("<");
    }

    protected virtual bool IsEndOfElement(string line)
    {
      return line.Equals(">");
    }


    protected virtual void ParseHeader(string line)
    {
      var header = new ElementHeader(line);
      ElementName = header.Name;
      ParseDefaultProperty(header.Values);
    }

    protected virtual void ParseDefaultProperty(string value)
    {
      DefaultProperty.ParseValue(value);
    }

    protected virtual void ParseContent()
    {
      ParseProperties();
    }


    protected string[] GetContent()
    {
      return Source.ToString()
        .Split(new[] {Environment.NewLine}, StringSplitOptions.None)
        .Where(s => s.Length > 0 && s != ">")
        .Select(s => s.StartsWith("<") ? s.Remove(0, 1) : s)
        .ToArray();
    }

    protected void ParseProperties()
    {
      var lines = 
        Source.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None)
          .Select(s => s.Trim())
          .Where(s => s.Length > 0 && s != ">");

      foreach (var line in lines)
      {
        var propertyName = line.GetWord();
        string propertyValue;
        if (propertyName.StartsWith("<"))
        {
          propertyName = propertyName.Remove(0, 1);
          ElementName = propertyName;
          propertyName = "_DEFAULT_";
          propertyValue = line.Remove(0, ElementName.Length + 1);
        }
        else
          propertyValue = line.Remove(0, propertyName.Length + 1);
        Properties.Add(new ReaperProperty(propertyName, propertyValue));
      }
    }


    protected ReaperProperty GetProperty(string propertyName)
    {
      var prop = Properties.FirstOrDefault(p => p.PropertyName.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
      if (prop == null)
      {
        prop = new ReaperProperty(propertyName, "");
        Properties.Add(prop);
      }
      return prop;
    }


    public IEnumerable<ElementBase> GetElements()
    {
      return GetElements(e => true);
    }
    
    public IEnumerable<ElementBase> GetElements(Predicate<ElementBase> p)
    {
      foreach (var child in ChildElements)
        if (p(child))
          yield return child;
    }

  }

}