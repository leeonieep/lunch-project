using LunchProject;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    const string title = "Lunch Spots API";

    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = title, Version = "v1" });
});
    
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAddLunchSpotService, AddLunchSpotService>();

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