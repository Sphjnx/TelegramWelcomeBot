using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

class WelcomeBot
{
    private static TelegramBotClient _botClient;

    static async Task Main()
    {
        // Replace "TOKEN YOU GET FROM BotFather" with your actual bot token.
        _botClient = new TelegramBotClient("TOKEN YOU GET FROM BotFather");

        var me = await _botClient.GetMeAsync();
        Console.WriteLine($"Start listening for @{me.Username}");

        int offset = 0;

        while (true)
        {
            var updates = await _botClient.GetUpdatesAsync(offset);

            foreach (var update in updates)
            {
                HandleUpdate(update);
                offset = update.Id + 1;
            }

            await Task.Delay(1000); 
        }
    }

    private static void HandleUpdate(Update update)
    {
        if (update.Type == UpdateType.Message)
        {
            var message = update.Message;

            if (message.Text != null)
            {
                Console.WriteLine($"Received a text message from {message.Chat.Id}: {message.Text}");
            }

            if (message.NewChatMembers != null)
            {
                foreach (var newMember in message.NewChatMembers)
                {
                    _botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"Welcome, {newMember.FirstName}! Welcome to the group! /n Rules:identify yourself with your name and your role in the group./n Have fun! "
                    );
                }
            }
        }
    }
}
