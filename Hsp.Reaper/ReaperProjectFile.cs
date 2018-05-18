using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Reaper;
using Hsp.Reaper.Elements;

namespace Hsp.Reaper
{

  public static class ReaperProjectFile
  {

    public static ReaperProject Load(String filename)
    {
      using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
        return Load(fs);
    }
    public static ReaperProject Load(Stream s)
    {
      ReaperElementFactory factory = new ReaperElementFactory();
      ReaperElement currElement = null;
      List<ReaperElement> elements = new List<ReaperElement>();

      using (StreamReader sr = new StreamReader(s))
        while (!sr.EndOfStream)
        {
          var line = sr.ReadLine();
          if (!String.IsNullOrEmpty(line))
          {
            bool isNewFx =
              line.StartsWith("BYPASS") &&
              (currElement != null) &&
              (currElement.ElementName == "FXCHAIN");

            if (line.StartsWith("<") || isNewFx)
            {
              if (isNewFx)
                currElement = currElement.ParentElement;
              currElement = factory.CreateElement(currElement, line);
              elements.Add(currElement);
            }
            currElement.Source.AppendLine(line);
            if (line.StartsWith(">"))
              currElement = currElement.ParentElement;
          }
        }

      elements.ForEach(e => { e.Parse(); });

      ReaperElement rootElement = elements[0];
      return rootElement as ReaperProject;
    }

  }
}
