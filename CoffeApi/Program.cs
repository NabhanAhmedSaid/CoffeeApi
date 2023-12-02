var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var CoffeeList = new List<Coffee> {
    new Coffee{ Id=1, Name="Mocha", Price=1.5},
     new Coffee{ Id=2, Name="Latte", Price=2.0},
     new Coffee{ Id=3, Name="Black Coffee", Price=1.2},
      new Coffee{ Id=4, Name="Americano", Price=1.3},
};

app.MapGet("/coffee",() =>{
    return CoffeeList;
});

app.MapGet("/coffee/{id}",(int id) =>{
    var coffee = CoffeeList.Find(c => c.Id==id);
    return coffee;
});

app.MapPost("/coffee",(Coffee coffee)=>
{
    CoffeeList.Add(coffee);
    return CoffeeList;
});

app.MapPut("/coffee/{id}",(Coffee UpdatedCoffee,int id)=>
{
    var coffee = CoffeeList.Find(c => c.Id==id);
    coffee.Name = UpdatedCoffee.Name;
    coffee.Price = UpdatedCoffee.Price;   
    
    return CoffeeList;
});

app.MapDelete("/coffee/{id}", (int id)=>
{
     var coffee = CoffeeList.Find(c => c.Id==id);
     CoffeeList.Remove(coffee);
     return CoffeeList;
});


app.Run();

public class Coffee
{
    public int Id { get; set; }
    public  string Name {get;set;}
    public double Price {get;set;}
}