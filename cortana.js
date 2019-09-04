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
    msg.channel.send('Version 1.00.00.01.');
    msg.channel.send('Created by SparrDrem, 2019-30-04 10:19:25:00');
  }
  if (msg.content === '!awdbios') {
    msg.channel.send('```   Award Modular BIOS v6.0, An Energy Star Ally\n   Copyright (C) 1984-2000, Award Software, Inc.\n\nASUS P4T ACPI BIOS Revision 1003\n\nIntel(R) Pentium(R) 4 1759 MHz Processor\nRDRAM Clock : 440 MHz\nMemory Test :  262144K OK\n\nAward Plug and Play BIOS Extension v1.0A\nInitialize Plug and Play Cards...\nPNP Init Completed\n\nTrend ChipAwayVirus(R) On Guard\n\nDetecting Primary Master  ... IBM-DTTA-371010\nDetecting Primary Slave   ... QUANTUM FIREBALL SE4.3A\nDetecting Secondary Master... [Press F4 to skip]_\n\n\n\n\nPress DEL to enter SETUP\n12/29/2000-1858-<<P4T>>-00```');
  }
  if (msg.content === '!test') {
    msg.channel.send(exampleEmbed);
  }
});

client.login('$dummy_token$');
