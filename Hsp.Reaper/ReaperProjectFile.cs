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

    public static ProjectElement Load(String filename)
    {
      using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
        return Load(fs);
    }

    public static ProjectElement Load(Stream s)
    {
      var factory = ElementFactory.Instance;
      var elements = new List<ElementBase>();

      using (var sr = new StreamReader(s))
      {
        var currLine = "";
        while (String.IsNullOrEmpty(currLine))
          currLine = sr.ReadLine();
        var projectElement = new ProjectElement(currLine);
        projectElement.Read(sr);
        return projectElement;
      }

      /*
      line = line.TrimStart();

          var isNewFx =
            line.StartsWith("BYPASS") &&
            currElement != null &&
            currElement.ElementName == "FXCHAIN";

          var startNewElement =
            line.StartsWith("<") ||
            isNewFx;

          if (startNewElement)
          {
            if (isNewFx)
              currElement = currElement.ParentElement;
            if (currElementContent != null)
            {
              currElement = factory.CreateElement(currElement, currElementContent.ToString());
              elements.Add(currElement);
            }
            currElementContent = new StringBuilder();
          }

          currElementContent?.AppendLine(line);
          if (line.StartsWith(">"))
            currElement = currElement?.ParentElement;
        }

      var rootElement = elements[0];
      return rootElement as ProjectElement;
    */
    }

  }

}
