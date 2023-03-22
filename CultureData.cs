using MCM.Abstractions.Base.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            UpdateCultureDataWithDominantCulture(settlementNewCulture, GlobalSettings<MCMConfig>.Instance.SettlementDailyMod);
            settlement.Culture = GetMainCulture();
            if (settlement.Notables.Count > 0)
                foreach (Hero notable in settlement.Notables)
                    notable.Culture = settlement.Culture;
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

        // TRY LATER
        /*private float kingdomDailyMod = 1f;
        public void UpdateCultureDataAsKingdom(Kingdom kingdom) {
            kingdom.Chan
        }*/

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
    }
}
