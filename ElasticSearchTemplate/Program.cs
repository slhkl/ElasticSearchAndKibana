using Business;
using Data.Model;
using ElasticSearchTemplate.StartupConfiguration;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddElasticSearch(builder.Configuration);
builder.Services.AddSingleton<ProductBusiness>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/Get/{keyword}", ([FromServices] ProductBusiness productBusiness, string keyword) =>
{
    return productBusiness.Get(keyword);
});

app.MapPost("/Post", ([FromServices]ProductBusiness productBusiness, [FromBody] Product product) =>
{
    productBusiness.Post(product);
    return "Ok";
});

app.MapDelete("/Delete/{id}", ([FromServices]ProductBusiness productBusines, int id) =>
{
    productBusines.Delete(id);
    return "Ok";
});

app.Run();