using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hsp.Reaper.Elements
{

  public class ReaperElement
  {
    protected List<ReaperProperty> Properties { get; set; }
    internal StringBuilder Source { get; private set; }
    internal IEnumerable<string> Content
    {
      get
      {
        return
          Source.ToString().Split(new String[] { Environment.NewLine }, StringSplitOptions.None).
          Where(s => (s.Length > 0) && (s != ">")).
          Select(s => s.StartsWith("<") ? s.Remove(0, 1) : s);
      }
    }

    internal virtual void Parse()
    {
      // do not auto-parse generic ReaperElements
      if (this.GetType() != typeof(ReaperElement))
        ParseProperties();
    }
    protected void ParseProperties()
    {
      IEnumerable<string> lines = 
        Source.ToString().Split(new String[] { Environment.NewLine }, StringSplitOptions.None)
        .Select(s => s.Trim())
        .Where(s => (s.Length > 0) && (s != ">"));
      foreach (var line in lines)
      {
        string propertyName = line.GetWord();
        string propertyValue = "";
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

    ReaperElement _ParentElement;
    public ReaperElement ParentElement
    {
      get { return _ParentElement; }
      internal set
      {
        if ((_ParentElement != null) && (_ParentElement.ChildElements.Contains(this)))
          _ParentElement.ChildElements.Remove(this);
        _ParentElement = value;
        if (_ParentElement != null)
          _ParentElement.ChildElements.Add(this);
      }
    }
    internal String ElementName { get; set; }
    public List<ReaperElement> ChildElements;
    public IEnumerable<string> PropertyNames
    {
      get
      {
        return Properties.Select(p => p.PropertyName).Where(p => { return p != "_DEFAULT_"; });
      }
    }
    protected ReaperProperty DefaultProperty
    {
      get { return GetProperty("_DEFAULT_"); }
    }

    internal ReaperElement(ReaperElement parentElement, string elementName = "")
    {
      Source = new StringBuilder();
      Properties = new List<ReaperProperty>();
      ChildElements = new List<ReaperElement>();
      ElementName = elementName;
      ParentElement = parentElement;
    }

    public void SetProperty(String propertyName, String propertyValue)
    {
      SetProperty(new ReaperProperty(propertyName, propertyValue));
    }
    public void SetProperty(ReaperProperty property)
    {
      if (Properties.Contains(property))
        return;
      Properties.Add(property);
    }
    public ReaperProperty GetProperty(String propertyName)
    {
      return Properties.FirstOrDefault(p => { return String.Compare(p.PropertyName, propertyName, true) == 0; });
    }
    public String GetPropertyValue(String propertyName, int index = 0, String defaultValue = "")
    {
      ReaperProperty prop = GetProperty(propertyName);
      return prop != null ? prop[index] : defaultValue;
    }
    public void SetPropertyValue(String propertyName, String value, int index = 0)
    {
      ReaperProperty prop = GetProperty(propertyName);
      if (prop == null)
        return;
      prop[index] = value;
    }

    public IEnumerable<ReaperElement> GetElements()
    {
      return GetElements(e => { return true; });
    }
    public IEnumerable<ReaperElement> GetElements(Predicate<ReaperElement> p)
    {
      List<ReaperElement> result = new List<ReaperElement>();
      ChildElements.ForEach(ce =>
        {
          if (p(ce))
            result.Add(ce);
          result.AddRange(ce.GetElements(p));
        });
      return result;
    }
  }
}
