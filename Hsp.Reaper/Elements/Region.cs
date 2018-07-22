namespace Hsp.Reaper.Elements
{

  public class Region : ElementBase
  {

    public int Id { get; set; }
    
    public double Start { get; set; }
    
    public double End { get; set; }
    
    public string Description { get; set; }


    public override string ToString()
    {
      return $"{Id}: {Description}";
    }

  }

}
