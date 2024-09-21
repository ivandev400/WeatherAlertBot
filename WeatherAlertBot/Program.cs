using Microsoft.EntityFrameworkCore;
using WeatherAlertBot.Controllers.Commands;
using WeatherAlertBot.Db;
using WeatherAlertBot.Services;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddScoped<IUserExistsService, UserExistsService>();
builder.Services.AddScoped<IReturnSettingsService, ReturnSettingsService>();
builder.Services.AddScoped<CreateUserService>();
builder.Services.AddScoped<ChangeUserSettingsService>();

builder.Services.AddScoped<SettingsCommand>(serviceProvider =>
    CommandFactory.CreateCommand<SettingsCommand>(serviceProvider));

builder.Services.AddScoped<StartCommand>(serviceProvider =>
    CommandFactory.CreateCommand<StartCommand>(serviceProvider));

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<UserContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));


builder.Logging.AddConsole();
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
