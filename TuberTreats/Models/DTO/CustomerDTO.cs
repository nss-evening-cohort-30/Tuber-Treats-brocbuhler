namespace TuberTreats.Models;

public class CustomerDTO
{
  int Id { get; set; }
  string Name { get; set; }
  string Address { get; set; }
  TuberOrder TuberOrders { get; set; }
}
