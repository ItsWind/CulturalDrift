using System;
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
                    CharacterObject? troopToSpawn = null;

                    if (sellerHero.IsRuralNotable && sellerHero.CurrentSettlement.Village.Bound.IsCastle) {
                        try {
                            troopToSpawn = cultureToSpawn.EliteBasicTroop;
                        }
                        catch (Exception) { }

                        if (troopToSpawn == null)
                            return settlementCultureData.DefaultCulture.EliteBasicTroop;
                    }
                    else {
                        try {
                            troopToSpawn = cultureToSpawn.BasicTroop;
                        }
                        catch (Exception) { }

                        if (troopToSpawn == null)
                            return settlementCultureData.DefaultCulture.BasicTroop;
                    }

                    return troopToSpawn;
                }
            }
            return base.GetBasicVolunteer(sellerHero);
        }
    }
}
