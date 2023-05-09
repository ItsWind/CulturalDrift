using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Settlements;

namespace CulturalDrift {
    public class NewVolunteerModel : DefaultVolunteerModel {
        public override CharacterObject GetBasicVolunteer(Hero sellerHero) {
            Settlement settlement = sellerHero.CurrentSettlement;
            if (settlement != null && (settlement.IsVillage || settlement.IsTown)) {
                CultureData settlementCultureData;
                try {
                    settlementCultureData = CulturalDriftBehavior.Instance.SettlementCultureData[settlement];
                }
                catch (KeyNotFoundException) {
                    settlementCultureData = new CultureData(settlement.Culture);
                    CulturalDriftBehavior.Instance.SettlementCultureData[settlement] = settlementCultureData;
                }
                CultureObject? cultureToSpawn = settlementCultureData.GetRandomSettlementSpawnCulture(settlement);
                if (cultureToSpawn != null) {
                    if (sellerHero.IsRuralNotable && sellerHero.CurrentSettlement.Village.Bound.IsCastle) {
                        return cultureToSpawn.EliteBasicTroop;
                    }
                    return cultureToSpawn.BasicTroop;
                }
            }
            return base.GetBasicVolunteer(sellerHero);
        }
    }
}
