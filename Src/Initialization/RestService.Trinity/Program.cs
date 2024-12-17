using Aplication;
using Infrastructure;
using RestService.Trinity.Configuration;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Host

builder.Host
    .configureSerilog("Information");

#endregion

builder.Services
    .Infrastructure()
    .RegisterMongo(configuration["AppSettings:CollectionName"],
                   configuration["AppSettings:DatabaseName"],      
                   configuration["AppSettings:ConnectionStringMongo"])
    .Application();

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

#region Logging

ILogger logger = app.Logger;

IHostApplicationLifetime lifetime = app.Lifetime;

lifetime.ApplicationStarted.Register(() =>
{

    logger.LogInformation("=============== PROGRAM =============");
});

#endregion Logging

await app.RunAsync();
