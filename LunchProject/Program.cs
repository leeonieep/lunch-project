using System.Text;
using LunchProject.Repositories;
using LunchProject.Services;
using LunchProject.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

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
builder.Services.AddScoped<IGetAllLunchSpotsService, GetAllLunchSpotsService>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();