namespace TuberTreats.Models;

public class TuberOrderDTO
{
  public int Id { get; set; }
  public DateTime OrderPlacedOnDate { get; set; }
  public int CustomerId { get; set; }
  public int TuberDriverId { get; set; }
  public DateTime DeliveredOnDate { get; set; }
  public Topping Toppings { get; set; }
  public CustomerDTO OrderCustomer { get; set; }
  public TuberDriverDTO OrderDriver { get; set; }
}
