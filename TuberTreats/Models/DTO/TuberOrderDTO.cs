namespace TuberTreats.Models;

public class TuberOrderDTO
{
  int Id { get; set; }
  DateTime OrderPlacedOnDate { get; set; }
  int CustomerId { get; set; }
  int TuberDriverId { get; set; }
  DateTime DeliveredOnDate { get; set; }
  Topping Toppings { get; set; }
}
