using Microsoft.EntityFrameworkCore;
using WeatherAlertBot.Controllers.Commands;
using WeatherAlertBot.Db;
using WeatherAlertBot.Services;
using WeatherAlertBot.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddScoped<ICommand, StartCommand>();
builder.Services.AddScoped<ICommand, CurrentWeatherCommand>();
builder.Services.AddScoped<ICommand, SettingsCommand>();

builder.Services.AddScoped<ReturnSettingsService>();
builder.Services.AddScoped<CommandExecutor>();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<UserContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
