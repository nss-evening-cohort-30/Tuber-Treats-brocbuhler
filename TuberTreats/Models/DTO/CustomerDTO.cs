namespace TuberTreats.Models;

public class CustomerDTO
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Address { get; set; }
  public TuberOrderDTO CustomerOrder { get; set; }
}
