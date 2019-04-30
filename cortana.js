var Discord = require('discord.io');
var logger = require('winston');
var auth = require('.\auth.json');
// Logger configurations
logger.remove(logger.transports.Console);
logger.add(new logger.transports.Console, {
        colorize: true
});
logger.level = 'debug';
// Initialize Cortana Payload
var bot = new Discord.Client({
        token: auth.token,
        autorun: true
});
bot.on('ready', function (evt) {
        logger.info('Connected');
        logger.info('Logged in as: ');
        logger.info(bot.username + ' - ('+ bot.id +')');
});
bot.on('message', function (user, userID, channelID, message, evt) {
        // Cortana will look for '!' in the chat
        if (message.substring(0, 1) == '!') {
                var args = message.substring(1).split('');
                var cmd = args[0];
                
                args = args.splice(1);
                switch(cmd) {
                        // Display current version
                    case 'ver':
                            bot.sendMessage({
                                    to: channelID,
                                    message: 'Cortana BOT for Discord. Version 1.00.00.01. Created by SparrDrem, 2019-30-04 10:19:25:00'
                            })
                    break;
                }
        }
});
