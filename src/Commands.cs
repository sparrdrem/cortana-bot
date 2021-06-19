// If you need to see how to use, check Example.cs

/*++
 * 
 * Commands.cs
 * 
 * Copyright (C) 2015-2021 SparrOSDeveloperTeam
 * Copyright (C) 2018-2021 SparrDrem
 * 
 * This is the commands file which are commands that users execute as a follow-up to the
 * bot prefix (or @mention) and the bot will do whatever it is instructed.
 * 
  --*/

using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cortana_bot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        // Placeholder for when I find out how to actually read a PE EXE's version
        public string version = "1.00.00.04";

        [Command("ver")]
        [Summary("")]
        private async Task Ver()
        {
            // This method displays the version of the bot

            // The following lines read a file called buildinfo.txt which is placed in the output directory at compile time which displays the date and time of compile.
            // TODO: Check if the compile time is created in the post-build event, in case the build event errors out.
            var buildInfo = Application.StartupPath + "\\buildinfo.txt";
            var reader = string.Join("", File.ReadAllLines(buildInfo, Encoding.UTF8));

            await Context.Channel.SendMessageAsync($"Cortana BOT for Discord\nVersion {version}.\nCreated by SparrDrem, {reader}");
        }

        [Command("version")]
        private async Task Version()
        {
            // This is a demonstration that you can actually call other methods to make a command exactly the same as the other without writing the same code.
            await Ver();
        }

        [Command("awdbios")]
        [Summary("")]
        private async Task AwdBios()
        {
            // Just a fun command that prints the AWARD Modular BIOS in a text block.
            await Context.Channel.SendMessageAsync("```   Award Modular BIOS v6.0, An Energy Star Ally\n   Copyright (C) 1984-2000, Award Software, Inc.\n\nASUS P4T ACPI BIOS Revision 1003\n\nIntel(R) Pentium(R) 4 1759 MHz Processor\nRDRAM Clock : 440 MHz\nMemory Test :  262144K OK\n\nAward Plug and Play BIOS Extension v1.0A\nInitialize Plug and Play Cards...\nPNP Init Completed\n\nTrend ChipAwayVirus(R) On Guard\n\nDetecting Primary Master  ... IBM-DTTA-371010\nDetecting Primary Slave   ... QUANTUM FIREBALL SE4.3A\nDetecting Secondary Master... [Press F4 to skip]_\n\n\n\n\nPress DEL to enter SETUP\n12/29/2000-1858-<<P4T>>-00```");
        }

        [Command("gencode")]
        private async Task Gencode()
        {
            // Based on an application I did years ago, it DMs you the code it generated unless the bot cannot DM you, in which case it will complain in the same channel the command was issued from.
            // TODO: Add code to make the bot reply to OP's msg w/o ping.
            // TODO: Import the original Gencode code, modified to be the full experience.
            var check = new Emoji("✅");      // The check emote is used to signify the DM was successful.
            var random = new Random();

            // We will try to send a message to the user. If it works then there should be a check mark on the original message.
            try
            {
                await Context.User.SendMessageAsync($"Your code is {random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}");
            }
        }

        [Command("test")]
        [Summary("")]
        private async Task Test()
        {
            // Just a test embed that exists for whatever reason, has no purpose to exist imo.
            EmbedBuilder testEmbed = new EmbedBuilder();
            testEmbed.WithTitle("Cortana Debug Menu");
            testEmbed.WithUrl("https://sparrdrem.github.io/cortana-bot");
            testEmbed.WithColor(Color.Blue);
            testEmbed.WithAuthor("SparrDrem", "https://avatars0.githubusercontent.com/u/40704566?s=200&v=4", "https://avatars0.githubusercontent.com/u/40704566?s=200&v=4");
            testEmbed.WithDescription("dummy");
            testEmbed.WithThumbnailUrl("https://avatars0.githubusercontent.com/u/40704566?s=200&v=4");
            testEmbed.AddField("Test Menu", false);
            testEmbed.WithImageUrl("https://avatars0.githubusercontent.com/u/40704566?s=200&v=4");
            testEmbed.WithFooter("This is a test embed menu, if this menu appears this means it is a success.", "https://avatars0.githubusercontent.com/u/40704566?s=200&v=4");
            await Context.Channel.SendMessageAsync("", false, testEmbed.Build());
        }

        [Command("src")]
        [Summary("")]
        private async Task Src()
        {
            // Source code link
            await Context.Channel.SendMessageAsync($":link:: https://sparrdrem.github.io/cortana-bot");
        }

        [Command("help")]
        [Summary("")]
        private async Task Help()
        {
            // This is the help section. You can create a help group if you want to create help sections for each command (see "example.cs" for proper setup).
            EmbedBuilder helpEmbed = new EmbedBuilder();
            helpEmbed.WithAuthor(Context.User);
            helpEmbed.WithColor(Color.Blue);
            helpEmbed.WithTitle("Cortana Bot");
            helpEmbed.AddField("`c!awdbios`", "Prints the AWARD Bios screen in text.", true);
            helpEmbed.AddField("`c!gencode`", "DMs you a 16 numbered randomized string (requires permissions to dm the user).", true);
            helpEmbed.AddField("`c!src`", "Sends the link to the source code for Cortana BOT.", true);
            helpEmbed.AddField("`c!ver`", "Prints the version of the bot.", true);

            await Context.Channel.SendMessageAsync("", false, helpEmbed.Build());
        }
    }
}