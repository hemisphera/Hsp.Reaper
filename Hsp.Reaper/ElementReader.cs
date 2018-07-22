using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsp.Reaper.Elements;

namespace Hsp.Reaper
{

  internal class ElementReader : IDisposable
  {

    public ElementReader ParentReader { get; }

    public string ElementName { get; private set; }

    private TextReader Reader { get; set; }

    private string FirstLine { get; set; }


    public ElementReader(TextReader reader, ElementReader parentReader = null)
      : this(reader, null, parentReader)
    {
    }

    public ElementReader(TextReader reader, string firstLine, ElementReader parentReader = null)
    {
      Reader = reader;
      FirstLine = firstLine;
      ParentReader = parentReader;
    }


    public ElementBase[] Read()
    {
      var lineCount = -1;
      var childElements = new List<ElementBase>();
      var buffer = new StringBuilder();

      string line;
      do
      {
        lineCount++;
        if (lineCount == 0 && !String.IsNullOrEmpty(FirstLine))
          line = FirstLine;
        else
          line = Reader.ReadLine();

        line = line.TrimStart();
        if (IsStartOfNewElement(line))
        {
          var childReader = new ElementReader(Reader, line, this);
          childElements.AddRange(childReader.Read());
        }
        else
          buffer.AppendLine(line);
      } while (!IsEndOfElement(line));

    }

    private bool IsEndOfElement(string line)
    {
      if (line.StartsWith(">")) return true;
      if (IsParentFxChain() && line.StartsWith("WAK")) return true;
      return false;
    }

    private bool IsStartOfNewElement(string line)
    {
      var isNewFx =
        line.StartsWith("BYPASS") &&
        ParentReader?.ElementName == "FXCHAIN";

      return 
        line.StartsWith("<") ||
        isNewFx;
    }


    private bool IsParentFxChain()
    {
      return ParentReader?.ElementName == "FXCHAIN";
    }


    public void Dispose()
    {
      Reader.Dispose();
    }

  }

}
