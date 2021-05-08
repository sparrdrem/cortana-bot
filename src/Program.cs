// Cortana's prefix is 'c!'

/*++
 * 
 * Program.cs
 * 
 * Copyright (C) 2015-2021 SparrOSDeveloperTeam
 * Copyright (C) 2018-2021 SparrDrem
 * 
 * This is the main executable program used for the bot. This is used to initialize
 * the bot.
 * 
  --*/

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading.Tasks;

namespace cortana_bot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBot().GetAwaiter().GetResult();

        public static DiscordSocketClient _client;
        private static CommandService _commands;
        private IServiceProvider _services;

        private BotConfig config;

        public async Task RunBot()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).BuildServiceProvider();

            if (!File.Exists("config.json"))
            {
                config = new BotConfig()
                {
                    prefix = "c!",
                    //token = "",
                    game = "c!help"
                };
                File.WriteAllText("config.json", JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            else
            {
                config = JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText("config.json"));
            }

            string game = config.game;

            string botToken = config.token;

            _client.Log += Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();

            await _client.SetGameAsync(game);

            Console.WriteLine($"Logged in as {_client.CurrentUser.Id}");

            await Task.Delay(-1);
        }

        private async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg);
            return Task.CompletedTask;
        }

        private async Task HandleCommandAsync(SocketMessage msg)
        {
            SocketGuild server = ((SocketGuildChannel)msg.Channel).Guild;

            string messageLower = msg.Content;
            var message = msg as SocketUserMessage;
            if (message is null || message.Author.IsBot) return;
            int argumentPos = 0;
            if (message.HasStringPrefix(config.prefix, ref argumentPos) || message.HasMentionPrefix(_client.CurrentUser, ref argumentPos))
            {
                var context = new SocketCommandContext(_client, message);
                var result = await _commands.ExecuteAsync(context, argumentPos, _services);
                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                    await message.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
    
    class BotConfig
    {
        public string token { get; set; }
        public string prefix { get; set; }
        public string game { get; set; }
    }
}