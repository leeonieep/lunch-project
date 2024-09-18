using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    const string title = "Lunch Spots API";

    c.SwaggerDoc("v3",
        new OpenApiInfo { Title = title, Version = "v3" });
});
    
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(swaggerUiOptions =>
{
    swaggerUiOptions.SwaggerEndpoint("/swagger/v3/swagger.json", "Lunch Spots API v3");
    swaggerUiOptions.SwaggerEndpoint("/swagger/internal-v3/swagger.json", "Lunch Spots API Internal v3");
    swaggerUiOptions.RoutePrefix = "docs"; 
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();