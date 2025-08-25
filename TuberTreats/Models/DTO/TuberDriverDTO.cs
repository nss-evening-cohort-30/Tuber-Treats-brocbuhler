namespace TuberTreats.Models;

public class TuberDriverDTO
{
  public int Id { get; set; }
  public string Name { get; set; }
  public TuberOrderDTO TuberDeliveries { get; set; }
}
