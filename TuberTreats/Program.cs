using Microsoft.AspNetCore.Authorization.Infrastructure;
using TuberTreats.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//data
List<Customer> customers = new List<Customer>
{
    new Customer()
    {
        Id = 1,
        Name = "Matt",
        Address = "1233 via ramon",
    },
    new Customer()
    {
        Id = 2,
        Name = "Ben",
        Address = "3244 Hickory ln",
    },
    new Customer()
    {
        Id = 3,
        Name = "Chris",
        Address = "342 Jay Station",
    },
    new Customer()
    {
        Id = 4,
        Name = "Jonah",
        Address = "3423 Drive",
    },
    new Customer()
    {
        Id = 5,
        Name = "Odie",
        Address = "9073 Teacher Ave",
    }
};
List<Topping> toppings = new List<Topping>
{
    new Topping()
    {
        Id = 1,
        Name = "Pepperoni",
    },
    new Topping()
    {
        Id = 2,
        Name = "Onion",
    },
    new Topping()
    {
        Id = 3,
        Name = "Mushroom",
    },
    new Topping()
    {
        Id = 4,
        Name = "Garlic",
    },
    new Topping()
    {
        Id = 5,
        Name = "Sausage",
    }
};
List<TuberDriver> tuberDrivers = new List<TuberDriver>
{
    new TuberDriver()
    {
        Id = 1,
        Name = "Carlyn",
    },
    new TuberDriver()
    {
        Id = 2,
        Name = "Barney",
    },
    new TuberDriver()
    {
        Id = 3,
        Name = "Broc",
    },
};
List<TuberOrder> tuberOrders = new List<TuberOrder>
{
    new TuberOrder()
    {
        Id = 1,
        OrderPlacedOnDate = new DateTime(2004, 12, 1),
        DeliveredOnDate = new DateTime(2004, 12, 1),
        CustomerId = 1,
        TuberDriverId = 1,
        Toppings = new List<Topping>
        {
            toppings.First(t => t.Id == 1),  // Pepperoni
            toppings.First(t => t.Id == 2)   // Onion
        }
    },
    new TuberOrder()
    {
        Id = 2,
        OrderPlacedOnDate = new DateTime(2004, 12, 1),
        DeliveredOnDate = new DateTime(2004, 12, 1),
        CustomerId = 3,
        TuberDriverId = 2,
        Toppings = new List<Topping>
        {
            toppings.First(t => t.Id == 3),  // Mushroom
            toppings.First(t => t.Id == 4)   // Garlic
        }
    },
    new TuberOrder()
    {
        Id = 3,
        OrderPlacedOnDate = new DateTime(2004, 12, 1),
        DeliveredOnDate = new DateTime(2004, 12, 1),
        CustomerId = 4,
        TuberDriverId = 3,
        Toppings = new List<Topping>
        {
            toppings.First(t => t.Id == 5),  // Sausage
            toppings.First(t => t.Id == 1)   // Pepperoni again
        }
    },
};

List<TuberTopping> tuberToppings = new List<TuberTopping> { };
//

//add endpoints here
//Tuber Orders
app.MapGet("/tuberorders", () =>
{ //I don't really understand the toppings field
    List<TuberOrderDTO> tuberOrder = tuberOrders.Select(t => new TuberOrderDTO
    {
        Id = t.Id,
        OrderPlacedOnDate = t.OrderPlacedOnDate,
        DeliveredOnDate = t.DeliveredOnDate,
        CustomerId = t.CustomerId,
        TuberDriverId = t.TuberDriverId,
        Toppings = t.Toppings.Select(st => st.Name).ToList()
    }).ToList();
    return Results.Ok(tuberOrder);
});
app.MapGet("/tuberorder/{id}", (int id) =>
{
    TuberOrder tuberOrder = tuberOrders.FirstOrDefault(st => st.Id == id);
    Customer orderCustomer = customers.FirstOrDefault(st => st.Id == tuberOrder.CustomerId);
    TuberDriver orderDriver = tuberDrivers.FirstOrDefault(st => st.Id == tuberOrder.TuberDriverId);
    if (tuberOrder == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(new TuberOrderDTO
    {
        Id = tuberOrder.Id,
        OrderPlacedOnDate = tuberOrder.OrderPlacedOnDate,
        DeliveredOnDate = tuberOrder.DeliveredOnDate,
        CustomerId = tuberOrder.CustomerId,
        OrderCustomer = orderCustomer == null ? null : new CustomerDTO
        {
            Id = orderCustomer.Id,
            Name = orderCustomer.Name,
            Address = orderCustomer.Address
        },
        TuberDriverId = tuberOrder.TuberDriverId,
        OrderDriver = orderDriver == null ? null : new TuberDriverDTO
        {
            Id = orderDriver.Id,
            Name = orderDriver.Name,
        },
        Toppings = tuberOrder.Toppings.Select(st => st.Name).ToList()
    });
});
app.MapPost("/tuberorders", (TuberOrder tuberOrder) =>
{
    Customer customer = customers.FirstOrDefault(st => st.Id == tuberOrder.CustomerId);
    TuberDriver tuberDriver = tuberDrivers.FirstOrDefault(st => st.Id == tuberOrder.TuberDriverId);
    if (customer == null || tuberDriver == null)
    {
        return Results.BadRequest();
    }
    tuberOrder.Id = tuberOrders.Max(st => st.Id) + 1;
    tuberOrders.Add(tuberOrder);
    return Results.Created($"/tuberorders/{tuberOrder.Id}", new TuberOrderDTO
    {
        Id = tuberOrder.Id,
        OrderPlacedOnDate = DateTime.Now,
        CustomerId = tuberOrder.CustomerId,
        OrderCustomer = customer == null ? null : new CustomerDTO
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address
        },
        TuberDriverId = tuberOrder.TuberDriverId,
        OrderDriver = tuberDriver == null ? null : new TuberDriverDTO
        {
            Id = tuberDriver.Id,
            Name = tuberDriver.Name
        },
        Toppings = tuberOrder.Toppings.Select(st => st.Name).ToList()
    });
});
app.MapPut("/tuberorders/{id}", (int id, int tuberDriverId) =>
{
    TuberOrder tuberOrder = tuberOrders.FirstOrDefault(st => st.Id == id);
    if (tuberOrder == null)
    {
        return Results.NotFound();
    }
    tuberOrder.TuberDriverId = tuberDriverId;
    tuberOrders.Add(tuberOrder); // needs to replace
    return Results.Created($"tuberorders/{tuberOrder.Id}", new TuberOrderDTO
    {
        Id = tuberOrder.Id,
        CustomerId = tuberOrder.CustomerId,
        TuberDriverId = tuberOrder.TuberDriverId,
        DeliveredOnDate = tuberOrder.DeliveredOnDate,
        OrderPlacedOnDate = tuberOrder.OrderPlacedOnDate,
        Toppings = tuberOrder.Toppings.Select(st => st.Name).ToList()
    });
});
app.MapPost("/tuberorders/{id}/complete", (int id) =>
{
    TuberOrder tuberOrder = tuberOrders.FirstOrDefault(st => st.Id == id);
    if (tuberOrder == null)
    {
        return Results.NotFound();
    }
    tuberOrder.DeliveredOnDate = DateTime.Now;
    tuberOrders.Add(tuberOrder); // needs to replace
    return Results.Created($"tuberorders/{tuberOrder.Id}/complete", new TuberOrderDTO
    {
        Id = tuberOrder.Id,
        CustomerId = tuberOrder.CustomerId,
        TuberDriverId = tuberOrder.TuberDriverId,
        DeliveredOnDate = tuberOrder.DeliveredOnDate,
        OrderPlacedOnDate = tuberOrder.OrderPlacedOnDate,
        Toppings = tuberOrder.Toppings.Select(st => st.Name).ToList()
    });
});
//

//toppings
app.MapGet("/toppings", () =>
{
    List<ToppingDTO> topping = toppings.Select(t => new ToppingDTO
    {
        Id = t.Id,
        Name = t.Name
    }).ToList();
    return Results.Ok(topping);
});

app.MapGet("/toppings/{id}", (int id) =>
{
    Topping topping = toppings.FirstOrDefault(st => st.Id == id);
    if (topping == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(new ToppingDTO
    {
        Id = topping.Id,
        Name = topping.Name,
    });
});
//

//tubertoppings
app.MapGet("/tubertoppings", () =>
{
    List<TuberToppingDTO> tuberTopping = tuberToppings.Select(t => new TuberToppingDTO
    {
        Id = t.Id,
        TuberOrderId = t.TuberOrderId,
        ToppingId = t.ToppingId
    }).ToList();
    return Results.Ok(tuberTopping);
});
app.MapPost("/tubertoppings", (TuberTopping tuberTopping) =>
{
    tuberTopping.Id = tuberOrders.Max(st => st.Id) + 1;
    tuberToppings.Add(tuberTopping);
    return Results.Created($"/tubertoppings/{tuberTopping.Id}", new TuberToppingDTO
    {
        Id = tuberTopping.Id,
        TuberOrderId = tuberTopping.TuberOrderId,
        ToppingId = tuberTopping.ToppingId
    });
});
app.MapPatch("/tubertoppings/{id}", (int id, int toppingId) =>
{
    TuberTopping tuberTopping = tuberToppings.FirstOrDefault(t => t.Id == id);
    if (tuberTopping == null)
    {
        return Results.NotFound();
    }
    tuberTopping.ToppingId = toppingId;
    tuberToppings.Add(tuberTopping); // needs to replace
    return Results.Created($"tubertoppings/{tuberTopping.Id}", new TuberToppingDTO
    {
        Id = tuberTopping.Id,
        TuberOrderId = tuberTopping.TuberOrderId,
        ToppingId = tuberTopping.ToppingId
    });
});
//

//customers
app.MapGet("/customers", () =>
{
    List<CustomerDTO> customer = customers.Select(t => new CustomerDTO
        {
            Id = t.Id,
            Name = t.Name,
            Address = t.Address
        }).ToList();
    return Results.Ok(customer);
});
app.MapGet("/customers/{id}", (int id) =>
{
    Customer customer = customers.FirstOrDefault(st => st.Id == id);
    TuberOrder tuberOrder = tuberOrders.FirstOrDefault(st => st.CustomerId == id);
    if (customer == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(new CustomerDTO
    {
        Id = customer.Id,
        Name = customer.Name,
        Address = customer.Address,
        CustomerOrder = tuberOrder == null ? null : new TuberOrderDTO
        {
            Id = tuberOrder.Id,
            DeliveredOnDate = tuberOrder.DeliveredOnDate,
            OrderPlacedOnDate = tuberOrder.OrderPlacedOnDate,
            TuberDriverId = tuberOrder.TuberDriverId,
            Toppings = tuberOrder.Toppings.Select(st => st.Name).ToList()
        },
    });
});
app.MapPost("/customers", (Customer customer) =>
{
    customer.Id = customers.Max(st => st.Id) + 1;
    customers.Add(customer);
    return Results.Created($"customers/{customer.Id}", new CustomerDTO
    {
        Id = customer.Id,
        Name = customer.Name,
        Address = customer.Address
    });
});
app.MapDelete("/customer/{id}", (int id) =>
{
    Customer customer = customers.FirstOrDefault(t => t.Id == id);
    if (customer == null)
    {
        return Results.NotFound();
    }
    customers.Remove(customer); // may not work properly I think I need to affect the customers list
    return Results.Ok();
});
//

//tuberdrivers
app.MapGet("/tuberdrivers", () =>
{
    List<TuberDriverDTO> tuberDriver = tuberDrivers.Select(t => new TuberDriverDTO
        {
            Id = t.Id,
            Name = t.Name,
        }).ToList();
    return Results.Ok(tuberDriver);
});
app.MapGet("/tuberdrivers/{id}", (int id) =>
{
    TuberDriver tuberDriver = tuberDrivers.FirstOrDefault(st => st.Id == id);
    TuberOrder tuberOrder = tuberOrders.FirstOrDefault(st => st.TuberDriverId == tuberDriver.Id);
    if (tuberDriver == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(new TuberDriverDTO
    {
        Id = tuberDriver.Id,
        Name = tuberDriver.Name,
        TuberDeliveries = tuberOrder == null ? null : new TuberOrderDTO
        {
            Id = tuberOrder.Id,
            OrderPlacedOnDate = tuberOrder.OrderPlacedOnDate,
            DeliveredOnDate = tuberOrder.DeliveredOnDate,
            CustomerId = tuberOrder.CustomerId,
            Toppings = tuberOrder.Toppings.Select(st => st.Name).ToList()
        },
    });
});
//

app.Run();
//don't touch or move this!
public partial class Program { }
