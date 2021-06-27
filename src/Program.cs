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
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace cortana_bot
{
    class Program
    {
        static void Main() => new Program().RunBot().GetAwaiter().GetResult();

        // We start by creating basic variables that we will reference throughout the script.
        public static DiscordSocketClient _client;
        private static CommandService _commands;
        private IServiceProvider _services;

        private BotConfig config;

        public async Task RunBot()
        {
            // We create definitions of the variables.
            _client = new DiscordSocketClient(new DiscordSocketConfig{
                ExclusiveBulkDelete = true
            }); // ExclusiveBulkDelete is set to suppress events when a bulk delete occurs.
            _commands = new CommandService();

            _services = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).BuildServiceProvider();

            // We will now export our bot's config to a json file. This is for simplicity purposes so that the file can be edited for later use.
            // Note: You can just remove the if statement and just hardcore define properties if you prefer to make it more secure.
            if (!File.Exists("config.json"))
            {
                config = new BotConfig()
                {
                    Prefix = "c!",        // Set your own prefix here, avoid common prefixes such as "!", "<", or "."
                    //Prefix2 = "C!",       // Duplicate of config.Prefix, 
                    Token = "",           // You must provide your own token
                    Game = "c!help"       // It is recommended to keep this as the prefix for people to know what its prefix is
                };
                File.WriteAllText("config.json", JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            else
            {
                config = JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText("config.json"));
            }

            string game = config.Game;

            string botToken = config.Token;

            // This is where the actual bot login process begins. If an error occurs the console should not print "Bot logged in successfully."
            _client.Log += Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();

            await _client.SetGameAsync(game);

            Console.WriteLine("Bot logged in successfully.");

            await Task.Delay(-1);
        }

        private async Task RegisterCommandsAsync()
        {
            // This allows the bot to look at messages sent any time one is sent.
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
        }

        private Task Log(LogMessage msg)
        {
            // This method simply logs warnings/errors/messages created by the bot.
            string logTime = (DateTime.Now).ToString("HH:mm:ss");  // Gets the current time and converts it to string.

            switch (msg.Exception)
            {
                case GatewayReconnectException gre:    // This is for when the bot must reset its connection after a while.
                    Console.WriteLine($"{logTime} Gateway     Server requested a reconnect.");
                    return Task.CompletedTask;
                case HttpException he:                 // This is for handling when the bot encounters HTTP code issues on Discord.
                    Console.WriteLine($"{logTime} Gateway     Unable to send message: Missing Permissions in channel.");        // Placeholder until I figure out a way to throw unique errors.
                    return Task.CompletedTask;
                default:
                    Console.WriteLine(msg);            // Normal logging.
                    return Task.CompletedTask;
            }
        }

        private async Task HandleCommandAsync(SocketMessage msg)
        {
            // This method reads messages sent that the bot can see, and handles them accordingly if it contains a ping at the beginning of the message or the bot's prefix.
            if (!(msg is SocketUserMessage message) || message.Author.IsBot) return;
            int argumentPos = 0;
            if (message.HasStringPrefix(config.Prefix, ref argumentPos) || message.HasMentionPrefix(_client.CurrentUser, ref argumentPos))
            //if (message.HasStringPrefix(config.Prefix, ref argumentPos) || message.HasStringPrefix(config.Prefix2, ref argumentPos) || message.HasMentionPrefix(_client.CurrentUser, ref argumentPos))
            // Uncomment the above line if more than one prefix is being used.
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
        // Class is used for the configuration to initialize the bot as seen above.
        public string Token { get; set; }
        public string Prefix { get; set; }
        //public string Prefix2 { get; set; }           // Duplicate of prefix to add a second prefix, being used programatically as "C!" since the bot doesn't accept ToLower for the prefix.
        public string Game { get; set; }
    }
}