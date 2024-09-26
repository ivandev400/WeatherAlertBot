﻿using Telegram.Bot;
using WeatherAlertBot.Models;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Services;
using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherAlertBot.Controllers.Commands
{
    public class StartCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/start";
        public string CommandDescription => CommandDescriptions.StartCommand;
        public ICreateUserService createUserService;

        public StartCommand(ICreateUserService createUserService)
        {
            this.createUserService = createUserService;
        }

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            createUserService.CreateUser(update);
            string textMessage = "This is Weather Alert Bot(or storm watch).This Bot was created to help you to know weather at time. Here you can use different commands shown below - immidiately send you message with weather info in location that you can see and set using command /settings (default location is Kyiv).\r\n\r\nЦе Weather Alert Bot (або Storm Watch). Цей бот був створений, щоб допомогти вам дізнатися погоду в даний момент у будь-якій точці світу та інші плюшки. Тут ви можете використовувати різні команди показані нижче.Бот може негайно надіслати вам повідомлення з інформацією про погоду в будь-якій точці світу, яке ви можете переглянути та вказати за допомогою команди /settings (за замовчуванням - Київ).";
            await Client.SendTextMessageAsync(chatId, textMessage, replyMarkup: Keyboard());
        }

        private ReplyKeyboardMarkup Keyboard()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] {"/currentweather", "/settings", "/help"}
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = false,
                InputFieldPlaceholder = "Choose an option"
            };
        }
    }
}
