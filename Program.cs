using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Configure the Redis database or IDatabase
   Then add it to the Dependency injection 
   Service
*/

// use connection multiplexer
// to connection with the connection string
// you can add it to appsettings.json
var redisurl = builder.Configuration["redis:redisurl"];
var redis = ConnectionMultiplexer.Connect(redisurl);
builder.Services.AddScoped<IDatabase>(s=>redis.GetDatabase());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
