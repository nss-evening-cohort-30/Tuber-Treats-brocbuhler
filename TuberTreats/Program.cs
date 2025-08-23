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
    },
    new TuberOrder()
    {
        Id = 2,
        OrderPlacedOnDate = new DateTime(2004, 12, 1),
        DeliveredOnDate = new DateTime(2004, 12, 1),
        CustomerId = 3,
        TuberDriverId = 2,
    },
    new TuberOrder()
    {
        Id = 3,
        OrderPlacedOnDate = new DateTime(2004, 12, 1),
        DeliveredOnDate = new DateTime(2004, 12, 1),
        CustomerId = 4,
        TuberDriverId = 3,
    },
};
List<TuberTopping> tuberToppings = new List<TuberTopping> { };
//

//add endpoints here
//Tuber Orders
app.MapGet("/tuberorders", () =>
{
    List<TuberOrderDTO> tuberOrder = tuberOrders.Select(t => new TuberOrderDTO
    {
        Id = t.Id,
        OrderPlacedOnDate = t.OrderPlacedOnDate,
        DeliveredOnDate = t.DeliveredOnDate,
        CustomerId = t.CustomerId,
        TuberDriverId = t.TuberDriverId
    }).ToList();
    return Results.Ok(tuberOrder);
});
app.MapGet("/tuberorder/{id}", (int id) =>
{
    //I dont quite understand tuberDeliveries field
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
        }
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
        }
    });
});
app.MapPut("/tuberorders/{id}", (TuberDriver tuberDriver) =>
{
    // TuberOrder tuberOrder = tuberOrders.
    // return Results.Created($"/tuberorders/{tuberid}", new TuberDriverDTO
    // {
    //     Id = tuber
    // });
});
app.MapPost("/tuberorders/{id}/complete", () =>
{

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

});
app.MapPost("/tubertoppings", () =>
{

});
app.MapDelete("/tubertoppings", () =>
{

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
            TuberDriverId = tuberOrder.TuberDriverId
        },
    });
});
app.MapPost("/customers", () =>
{

});
app.MapDelete("/customer/{id}", (int id) =>
{

});
//

//tuberdrivers
app.MapGet("/tuberdrivers", () =>
{

});
// app.MapGet("/tuberdrivers/{id}", (int id) =>
// {

// });'
//by tuber delivery somehow
//

//

app.Run();
//don't touch or move this!
public partial class Program { }
