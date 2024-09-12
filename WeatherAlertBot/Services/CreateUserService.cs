﻿using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Services
{
    public class CreateUserService
    {
        private readonly UserContext _userContext;
        private Logger<IfUserExistsService> _logger;
        private IfUserExistsService _userExistsService;

        public bool CreateUser(Update update)
        {
            if (_userExistsService.UserExistsByUpdate(update))
            {
                _logger.LogWarning("The user already exists");
                return false;
            }
            if(update.Message != null)
            {
                var newUser = new Models.User
                {
                    ChatId = update.Message.Chat.Id,
                    UserSettings = new UserSettings()
                };

                _userContext.Users.Add(newUser);
                _userContext.SaveChanges();
            }
            return true;
        }
    }
}
