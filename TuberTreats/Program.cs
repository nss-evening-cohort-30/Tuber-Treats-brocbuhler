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
    new Customer
    {
        Id = ,
        Name = ,
        Address = ,
        TuberOrders - ,
    },
    {
        Id = ,
        Name = ,
        Address = ,
        TuberOrders - ,
    },
    {
        Id = ,
        Name = ,
        Address = ,
        TuberOrders - ,
    },
    {
        Id = ,
        Name = ,
        Address = ,
        TuberOrders - ,
    },
    {
        Id = ,
        Name = ,
        Address = ,
        TuberOrders - ,
    }
};
List<Topping> toppings = new List<Topping> { };
List<TuberDriver> tuberDrivers = new List<TuberDriver> { };
List<TuberOrder> tuberOrders = new List<TuberOrder> { };
List<TuberTopping> tuberToppings = new List<TuberTopping> { };
//
//add endpoints here
//Tuber Orders

//

app.Run();
//don't touch or move this!
public partial class Program { }
