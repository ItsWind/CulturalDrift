using MCM.Abstractions.Base.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.SaveSystem;

namespace CulturalDrift {
    public class CultureData {
        public class CultureFloat {
            [SaveableField(1)]
            public CultureObject Culture;
            [SaveableField(2)]
            public float Value;

            public CultureFloat(CultureObject culture, float value) {
                Culture = culture;
                Value = value;
            }

            public void ModifyValue(float mod) {
                Value += mod;

                if (Value < 0f)
                    Value = 0f;
                else if (Value > 100f)
                    Value = 100f;
            }
        }

        [SaveableField(1)]
        public CultureObject DefaultCulture;
        [SaveableField(2)]
        public List<CultureFloat> CultureFloats = new();

        public CultureData(CultureObject defaultCulture) {
            DefaultCulture = defaultCulture;
            CultureFloats.Add(new CultureFloat(defaultCulture, 100f));
        }

        public void UpdateCultureDataAsSettlement(Settlement settlement) {
            CultureObject? settlementNewCulture = null;
            try {
                settlementNewCulture = settlement.Owner.Culture;
            }
            catch (Exception) { return; }

            float modToUse = settlement.IsCastle ? GlobalSettings<MCMConfig>.Instance.CastleDailyMod : GlobalSettings<MCMConfig>.Instance.TownDailyMod;

            UpdateCultureDataWithDominantCulture(settlementNewCulture, modToUse);
            settlement.Culture = GetMainCulture();
            UpdateSettlementNotablesCulture(settlement);
        }

        public void UpdateCultureDataAsClan(Clan clan) {
            if (clan.Heroes.Count < 1)
                return;

            Dictionary<CultureObject, int> amountsOfCulture = new();
            foreach (Hero hero in clan.Heroes) {
                if (hero.Culture == null)
                    continue;

                if (!amountsOfCulture.ContainsKey(hero.Culture))
                    amountsOfCulture[hero.Culture] = 1;
                else
                    amountsOfCulture[hero.Culture] += 1;
            }

            CultureObject clanNewCulture = amountsOfCulture.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            UpdateCultureDataWithDominantCulture(clanNewCulture, GlobalSettings<MCMConfig>.Instance.ClanDailyMod);
            clan.Culture = GetMainCulture();
        }

        public CultureObject GetMainCulture() {
            return CultureFloats.Aggregate((l, r) => l.Value > r.Value ? l : r).Culture;
        }

        private void UpdateCultureDataWithDominantCulture(CultureObject domCulture, float modToUse) {
            bool needToCreate = true;

            foreach (CultureFloat cf in CultureFloats) {
                if (cf.Culture != domCulture)
                    cf.ModifyValue(-modToUse);
                else {
                    needToCreate = false;
                    cf.ModifyValue(modToUse);
                }
            }

            if (needToCreate)
                CultureFloats.Add(new CultureFloat(domCulture, modToUse));
        }

        private void UpdateSettlementNotablesCulture(Settlement settlement) {
            if (settlement.Notables.Count <= 0)
                return;

            int settlementTroopWeight = GlobalSettings<MCMConfig>.Instance.SettlementCultureTroopSpawnWeight;
            int defaultTroopWeight = GlobalSettings<MCMConfig>.Instance.DefaultCultureTroopSpawnWeight;
            int clanTroopWeight = GlobalSettings<MCMConfig>.Instance.OwnerClanCultureTroopSpawnWeight;
            int kingdomTroopWeight = GlobalSettings<MCMConfig>.Instance.KingdomCultureTroopSpawnWeight;

            int totalWeight = settlementTroopWeight + defaultTroopWeight + clanTroopWeight + kingdomTroopWeight;

            Dictionary<(int, int), CultureObject> cultureMap = new();
            cultureMap[(1, settlementTroopWeight)] = settlement.Culture;

            int currentMin = settlementTroopWeight;
            if (defaultTroopWeight > 0 && DefaultCulture != null) {
                cultureMap[(currentMin, currentMin + defaultTroopWeight)] = DefaultCulture;
                currentMin += defaultTroopWeight;
            }
            
            if (settlement.OwnerClan != null) {
                if (clanTroopWeight > 0) {
                    cultureMap[(currentMin, currentMin + clanTroopWeight)] = settlement.OwnerClan.Culture;
                    currentMin += clanTroopWeight;
                }

                if (kingdomTroopWeight > 0 && settlement.OwnerClan.Kingdom != null)
                    cultureMap[(currentMin, currentMin + kingdomTroopWeight)] = settlement.OwnerClan.Kingdom.Culture;
            }

            foreach (Hero notable in settlement.Notables) {
                int randNum = SubModule.Random.Next(1, totalWeight);

                foreach (KeyValuePair<(int, int), CultureObject> kvp in cultureMap) {
                    int min = kvp.Key.Item1;
                    int max = kvp.Key.Item2;

                    if (randNum >= min && randNum < max) {
                        notable.Culture = kvp.Value;
                        break;
                    }
                }
            }
        }
    }
}
