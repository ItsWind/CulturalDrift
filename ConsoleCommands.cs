using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;

namespace CulturalDrift {
    public class ConsoleCommands {
        /*
        [CommandLineFunctionality.CommandLineArgumentFunction("reloadconfig", "bastardchildren")]
        private static string CommandReloadConfig(List<string> args) {
            SubModule.Config.LoadConfig();
            return "Config reloaded!";
        }
        */

        [CommandLineFunctionality.CommandLineArgumentFunction("debug_print_clan_culture", "culturaldrift")]
        private static string DebugPrintClanCulture(List<string> args) {
            if (args.Count < 1) {
                return "Need clan name.";
            }
            else {
                string clanNameGiven = args[0];
                foreach (Clan clan in Campaign.Current.Clans)
                    if (clan.Name.ToString().ToLower().Replace(" ", "") == clanNameGiven.ToLower())
                        return clan.Culture.GetName().ToString();
                return "No clan with that name found. Try with no spaces and check for spelling.";
            }
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("debug_print_settlement_culture", "culturaldrift")]
        private static string DebugPrintSettlementCulture(List<string> args) {
            if (args.Count < 1) {
                return "Need settlement name.";
            } else {
                string settlementNameGiven = args[0];
                foreach (Settlement settlement in Campaign.Current.Settlements)
                    if (settlement.Name.ToString().ToLower().Replace(" ", "") == settlementNameGiven.ToLower())
                        return settlement.Culture.GetName().ToString();
                return "No settlement with that name found. Try with no spaces and check for spelling.";
            }
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("debug_change_hero_culture", "culturaldrift")]
        private static string DebugPopBabyWithWife(List<string> args) {
            if (args.Count < 2) {
                return "Proper usage: culturaldrift.debug_change_hero_culture HeroNameToChangeCulture HeroNameToCopyCultureFrom.";
            } else {
                string heroNameToChange = args[0].ToLower();
                string heroNameToCopyCultureFrom = args[1].ToLower();
                Hero? heroToChange = null;
                Hero? heroToCopyCultureFrom = null;

                foreach (Hero hero in Campaign.Current.AliveHeroes) {
                    string heroName = hero.GetName().ToString().ToLower().Replace(" ", "");
                    if (heroName == heroNameToChange)
                        heroToChange = hero;
                    else if (heroName == heroNameToCopyCultureFrom)
                        heroToCopyCultureFrom = hero;
                }

                if (heroToChange == null)
                    return "First hero name given not found. Try without spaces and check for spelling.";
                else if (heroToCopyCultureFrom == null)
                    return "Second hero name given not found. Try without spaces and check for spelling.";
                else if (heroToCopyCultureFrom.Culture == null)
                    return "Second hero has no valid culture to copy from.";

                heroToChange.Culture = heroToCopyCultureFrom.Culture;
                return heroToChange.Name.ToString() + " changed to " + heroToCopyCultureFrom.Culture.Name.ToString();
            }
        }
    }
}