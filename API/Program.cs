using API.Configuration;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddCustomCors(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCustomSwagger();

builder.Services.AddCustomControllers();

builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddApplicationLayer();

builder.Services.AddInfrastructureLayer(builder.Configuration);

var app = builder.Build();

app.UseInfrastructureLayer(app.Services);

app.UseCustomSwagger(app.Environment);

app.UseHttpsRedirection();

app.UseCustomCors();

app.UseAuthorization();

app.UseCustomControllers();

app.Run();
