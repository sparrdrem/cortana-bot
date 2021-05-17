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
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cortana_bot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        public string version = "1.00.00.04";

        [Command("ver")]
        [Summary("")]
        private async Task Ver()
        {
            var buildInfo = Application.StartupPath + "\\buildinfo.txt";
            var reader = string.Join("", File.ReadAllLines(buildInfo, Encoding.UTF8));

            await Context.Channel.SendMessageAsync($"Cortana BOT for Discord\nVersion {version}.\nCreated by SparrDrem, {reader}");
        }

        [Command("awdbios")]
        [Summary("")]
        private async Task AwdBios()
        {
            await Context.Channel.SendMessageAsync("```   Award Modular BIOS v6.0, An Energy Star Ally\n   Copyright (C) 1984-2000, Award Software, Inc.\n\nASUS P4T ACPI BIOS Revision 1003\n\nIntel(R) Pentium(R) 4 1759 MHz Processor\nRDRAM Clock : 440 MHz\nMemory Test :  262144K OK\n\nAward Plug and Play BIOS Extension v1.0A\nInitialize Plug and Play Cards...\nPNP Init Completed\n\nTrend ChipAwayVirus(R) On Guard\n\nDetecting Primary Master  ... IBM-DTTA-371010\nDetecting Primary Slave   ... QUANTUM FIREBALL SE4.3A\nDetecting Secondary Master... [Press F4 to skip]_\n\n\n\n\nPress DEL to enter SETUP\n12/29/2000-1858-<<P4T>>-00```");
        }

        [Command("test")]
        [Summary("")]
        private async Task Test()
        {
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
            await Context.Channel.SendMessageAsync($":link:: https://sparrdrem.github.io/cortana-bot");
        }

        [Command("help")]
        [Summary("")]
        private async Task Help()
        {
            EmbedBuilder helpEmbed = new EmbedBuilder();
            helpEmbed.WithAuthor(Context.User);
            helpEmbed.WithColor(Color.Blue);
            helpEmbed.WithTitle("Cortana Bot");
            helpEmbed.AddField("`c!awdbios`", "Prints the AWARD Bios screen in text.", true);
            helpEmbed.AddField("`c!ver`", "Prints the version of the bot.", true);

            await Context.Channel.SendMessageAsync("", false, helpEmbed.Build());
        }
    }
}