using LunchProject.Repositories;
using LunchProject.Services;
using LunchProject.Services.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    const string title = "Lunch Spots API";

    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = title, Version = "v1" });
    
    c.EnableAnnotations();
});
    
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAddLunchSpotService, AddLunchSpotService>();
builder.Services.AddScoped<IFindLunchSpotService, FindLunchSpotService>();
builder.Services.AddScoped<IDeleteLunchSpotService, DeleteLunchSpotService>();
builder.Services.AddScoped<ILunchSpotRepository, LunchSpotRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(swaggerUiOptions =>
{
    swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    swaggerUiOptions.RoutePrefix = "docs"; 
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();