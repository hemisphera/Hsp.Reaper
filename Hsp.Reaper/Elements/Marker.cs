namespace Hsp.Reaper.Elements
{

  public class Marker : ElementBase
  {
    
    public int Id { get; set; }

    public double Position { get; set; }

    public override string ToString()
    {
      return $"{Id}: {Position}";
    }

  }

}
