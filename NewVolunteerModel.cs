using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Settlements;

namespace CulturalDrift {
    public class NewVolunteerModel : DefaultVolunteerModel {
        public override CharacterObject GetBasicVolunteer(Hero sellerHero) {
            Settlement settlement = sellerHero.CurrentSettlement;
            if (settlement != null && (settlement.IsVillage || settlement.IsTown)) {
                CultureData settlementCultureData = CulturalDriftBehavior.SettlementCultureData[settlement];
                if (settlementCultureData != null) {
                    CultureObject? cultureToSpawn = settlementCultureData.GetRandomSettlementSpawnCulture(settlement);
                    if (cultureToSpawn != null) {
                        if (sellerHero.IsRuralNotable && sellerHero.CurrentSettlement.Village.Bound.IsCastle) {
                            return cultureToSpawn.EliteBasicTroop;
                        }
                        return cultureToSpawn.BasicTroop;
                    }
                }
            }
            return base.GetBasicVolunteer(sellerHero);
        }
    }
}
