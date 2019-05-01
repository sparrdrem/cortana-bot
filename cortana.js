const Discord = require('discord.js');
const client = new Discord.Client();

client.on('ready', () => {
  console.log(`Logged in as ${client.user.tag}!`);
});

client.on('message', msg => {
  if (msg.content === '!ver') {
    msg.channel.send('Cortana BOT for Discord.');
    msg.channel.send('Version 1.00.00.01.');
    msg.channel.send('Created by SparrDrem, 2019-30-04 10:19:25:00');
  }
});

client.login('$dummy_token$');
