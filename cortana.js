const Discord = require('discord.js');
const client = new Discord.Client();
const exampleEmbed = new Discord.RichEmbed()
  .setColor('#0099ff')
  .setTitle('Cortana Debug Menu')
  .setURL('https://sparrdrem.github.io/cortana-bot')
  .setAuthor('SparrDrem', 'https://avatars0.githubusercontent.com/u/40704566?s=200&v=4', 'https://avatars0.githubusercontent.com/u/40704566?s=200&v=4')
  .setDescription('$dummy')
  .setThumbnail('https://avatars0.githubusercontent.com/u/40704566?s=200&v=4')
  .addField('Test menu')
  .setImage('https://avatars0.githubusercontent.com/u/40704566?s=200&v=4')
  .setFooter('This is a test embed menu, if this menu appears this means it is a success.', 'https://avatars0.githubusercontent.com/u/40704566?s=200&v=4')

client.on('ready', () => {
  console.log(`Logged in as ${client.user.tag}!`);
});

client.on('message', msg => {
  if (msg.content === '!ver') {
    msg.channel.send('Cortana BOT for Discord.');
    msg.channel.send('Version 1.00.00.01b.');
    msg.channel.send('Created by SparrDrem, 2019-30-04 10:19:25:00');
  }
  if (msg.content === '!test') {
    msg.channel.send(exampleEmbed);
  }
});

client.login('$dummy_token$');
