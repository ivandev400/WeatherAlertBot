﻿using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Services
{
    public class ChangeUserSettingsService : IChangeUserSettingsService
    {
        private IUserExistsService userExistsService;
        private UserContext userContext;

        public ChangeUserSettingsService(IUserExistsService userExistsService, UserContext userContext)
        {
            this.userExistsService = userExistsService;
            this.userContext = userContext;
        }

        public void ChangeUserSettingsLocation(User user, string location)
        {
            try
            {
                if (userExistsService.UserExistsByUser(user))
                {
                    var settings = userContext.UserSettings.First(u => u.UserId == user.Id);
                    settings.Location = location;
                    userContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public void ChangeUserSettingsUpdateInterval(User user, string updateInterval)
        {
            try
            {
                if (userExistsService.UserExistsByUser(user))
                {
                    var settings = userContext.UserSettings.First(u => u.UserId == user.Id);
                    settings.UpdateInterval = updateInterval;
                    userContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public void ChangeUserSettingsMorningTime(User user, TimeOnly morningTime)
        {
            try
            {
                if (userExistsService.UserExistsByUser(user))
                {
                    var settings = userContext.UserSettings.First(u => u.UserId == user.Id);
                    settings.MorningTime = morningTime;
                    userContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return; 
            }
        }
    }
}
