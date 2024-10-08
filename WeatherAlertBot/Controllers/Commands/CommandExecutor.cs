﻿using WeatherAlertBot.Interfaces;
using Telegram.Bot.Types;

namespace WeatherAlertBot.Controllers.Commands
{
    public class CommandExecutor : ITelegramUpdateListener
    {
        private readonly IEnumerable<ICommand> commands;
        private IListener? listener = null;

        public CommandExecutor(IEnumerable<ICommand> commands)
        {
            this.commands = commands;  
            foreach(var command in commands)
            {
                switch (command)
                {
                    case ChangeLocationCommand changeLocationCommand:
                        changeLocationCommand.Executor = this;
                        break;
                    case ChangeMorningTimeCommand changeMorningTimeCommand:
                        changeMorningTimeCommand.Executor = this;
                        break;
                    case AnableNotificationCommand anableNotificationCommand:
                        anableNotificationCommand.Executor = this;
                        break;
                    case LanguageCommand languageCommand:
                        languageCommand.Executor = this;
                        break;
                    case StartCommand startCommand:
                        startCommand.Executor = this;
                        break;
                }
            }
        }
        public async Task GetUpdate(Update update)
        {
            if (listener == null)
            {
                await ExecuteCommand(update);
            }
            else
            {
                await listener.GetUpdate(update);
            }
        }

        private async Task ExecuteCommand(Update update)
        {
            Message message = update.Message;
            foreach (var command in commands)
            {
                if (command.CommandName == message?.Text)
                {
                    await command.Execute(update);
                }
            }
        }

        public void StartListen(IListener newlistener)
        {
            listener = newlistener;
        }
        public void StopListen()
        {
            listener = null;
        }
    }
}
