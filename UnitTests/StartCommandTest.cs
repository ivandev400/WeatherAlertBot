using Moq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherAlertBot.Controllers.Commands;

namespace UnitTests
{
    public class StartCommandTest
    {
        [Fact]
        public async Task StartCommandTest_ShouldReturnExpectedMessage()
        {
            //Arrange
            var botClientMock = new Mock<ITelegramBotClient>();
            var startCommand = new StartCommand();
            var message = new Message { Chat = new Chat { Id = 11223 } };
            var update = new Update { Message = message };

            //Act
            await startCommand.Execute(update);

            //Assert
            botClientMock.Verify(
                client => client.SendTextMessageAsync(
                    11223,
                    It.IsAny<string>(),
                    It.IsAny<ParseMode>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<int>(),
                    It.IsAny<bool>(),
                    It.IsAny<IReplyMarkup>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}