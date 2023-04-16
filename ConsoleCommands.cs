using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

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
        private static string DebugChangeHeroCulture(List<string> args) {
            if (args.Count < 2) {
                return "Proper usage: culturaldrift.debug_change_hero_culture HeroNameToChangeCulture CultureName.";
            } else {
                string givenHeroNameToChange = args[0].ToLower();
                string givenCultureName = args[1].ToLower();
                Hero? heroToChange = null;
                CultureObject? cultureToChangeTo = null;

                foreach (Hero hero in Campaign.Current.AliveHeroes) {
                    string heroName = hero.Name.ToString().ToLower().Replace(" ", "");
                    if (heroName == givenHeroNameToChange) {
                        heroToChange = hero;
                        break;
                    }
                }

                foreach (CultureObject culture in MBObjectManager.Instance.GetObjectTypeList<CultureObject>()) {
                    string cultureName = culture.Name.ToString().ToLower().Replace(" ", "");
                    if (cultureName == givenCultureName) {
                        cultureToChangeTo = culture;
                        break;
                    }
                }

                if (heroToChange == null)
                    return "Hero name given not found. Try without spaces and check for spelling.";
                else if (cultureToChangeTo == null)
                    return "Culture name given not found. Try without spaces and check for spelling.";

                heroToChange.Culture = cultureToChangeTo;
                return heroToChange.Name.ToString() + " changed to " + cultureToChangeTo.Name.ToString();
            }
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("debug_change_clan_culture", "culturaldrift")]
        private static string DebugChangeClanCulture(List<string> args) {
            if (args.Count < 2) {
                return "Proper usage: culturaldrift.debug_change_clan_culture ClanNameToChangeCulture CultureName.";
            } else {
                string givenClanName = args[0].ToLower();
                string givenCultureName = args[1].ToLower();
                Clan? clanToChange = null;
                CultureObject? cultureToChangeTo = null;

                foreach (Clan clan in Campaign.Current.Clans) {
                    string clanName = clan.Name.ToString().ToLower().Replace(" ", "");
                    if (clanName == givenClanName) {
                        clanToChange = clan;
                        break;
                    }
                }

                foreach (CultureObject culture in MBObjectManager.Instance.GetObjectTypeList<CultureObject>()) {
                    string cultureName = culture.Name.ToString().ToLower().Replace(" ", "");
                    if (cultureName == givenCultureName) {
                        cultureToChangeTo = culture;
                        break;
                    }
                }

                if (clanToChange == null)
                    return "Clan name given not found. Try without spaces and check for spelling.";
                else if (cultureToChangeTo == null)
                    return "Culture name given not found. Try without spaces and check for spelling.";

                CulturalDriftBehavior.ClanCultureData[clanToChange].ForceCultureData(cultureToChangeTo);
                clanToChange.Culture = cultureToChangeTo;
                return clanToChange.Name.ToString() + " changed to " + cultureToChangeTo.Name.ToString();
            }
        }
    }
}