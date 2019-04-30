var Discord = require('discord.io');
var logger = require('winston');
var auth = require('.\auth.json');
// Logger configurations
logger.remove(logger.transports.Console);
logger.add(new logger.transports.Console, {
    colorize: true
});
logger.level = 'debug
