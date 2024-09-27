using Microsoft.EntityFrameworkCore;
using WeatherAlertBot.Controllers.Commands;
using WeatherAlertBot.Db;
using WeatherAlertBot.Services;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddDbContext<UserContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

builder.Services.AddScoped<IUserExistsService, UserExistsService>();
builder.Services.AddScoped<IReturnSettingsService, ReturnSettingsService>();
builder.Services.AddScoped<ICreateUserService, CreateUserService>();
builder.Services.AddScoped<IChangeUserSettingsService, ChangeUserSettingsService>();
builder.Services.AddScoped<IGetUserService, GetUserService>();
builder.Services.AddScoped<IMorningNotificationService, MorningNotificationService>();

builder.Services.AddTransient<ICommand, StartCommand>();
builder.Services.AddTransient<ICommand, CurrentWeatherCommand>();
builder.Services.AddTransient<ICommand, SettingsCommand>();
builder.Services.AddTransient<ICommand, ChangeLocationCommand>();
builder.Services.AddTransient<ICommand, ChangeMorningTimeCommand>();
builder.Services.AddTransient<ICommand, DailyWeatherCommand>();
builder.Services.AddTransient<ICommand, AnableNotificationCommand>();
builder.Services.AddTransient<ICommand, HelpCommand>();

builder.Services.AddTransient<IReplyKeyboard, ReplyKeyboard>();

builder.Services.AddTransient<IListener, ChangeLocationCommand>();
builder.Services.AddTransient<IListener, ChangeMorningTimeCommand>();

builder.Services.AddSingleton<IDailyNotifier, DailyNotifier>();

builder.Services.AddTransient<CommandExecutor>();
builder.Services.AddSingleton<UpdateDistributor<CommandExecutor>>();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddHostedService<NotificationBackgroundService>();

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
