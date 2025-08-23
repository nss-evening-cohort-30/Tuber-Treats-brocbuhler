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
    return tuberOrders;
});
app.MapGet("/tuberorder/{id}", (int id) =>
{

});
app.MapPost("/tuberorders", () =>
{

});
app.MapPut("/tuberorders", () =>
{

});
app.MapPost("/tuberorders/{id}/complete", () =>
{

});
//

//toppings
app.MapGet("/toppings", () =>
{
    List<ToppingDTO> tops = toppings.Select(t => new ToppingDTO
    {
        Id = t.Id,
        Name = t.Name
    }).ToList();
    return Results.Ok(tops);
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

});
app.MapGet("/customers/{id}", (int id) =>
{

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
