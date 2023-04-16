using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.SaveSystem;

namespace CulturalDrift {
    public class CulturalDriftBehavior : CampaignBehaviorBase {
        public static Dictionary<Settlement, CultureData> SettlementCultureData = new();
        public static Dictionary<Clan, CultureData> ClanCultureData = new();

        public override void RegisterEvents() {
            CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, (settlement) => {
                if (settlement.Culture == null)
                    return;

                if (!SettlementCultureData.ContainsKey(settlement))
                    SettlementCultureData[settlement] = new CultureData(settlement.Culture);

                SettlementCultureData[settlement].UpdateCultureDataAsSettlement(settlement);
            });
            CampaignEvents.DailyTickClanEvent.AddNonSerializedListener(this, (clan) => {
                if (clan.Culture == null)
                    return;

                if (!ClanCultureData.ContainsKey(clan))
                    ClanCultureData[clan] = new CultureData(clan.Culture);
                
                ClanCultureData[clan].UpdateCultureDataAsClan(clan);
            });
        }

        public override void SyncData(IDataStore dataStore) {
            dataStore.SyncData("SettlementCultureData", ref SettlementCultureData);
            dataStore.SyncData("ClanCultureData", ref ClanCultureData);

            if (dataStore.IsLoading) {
                // Set settlement cultures, as they do not save/load on their own.
                foreach (KeyValuePair<Settlement, CultureData> kvp in CulturalDriftBehavior.SettlementCultureData)
                    kvp.Key.Culture = kvp.Value.GetMainCulture();
            }
        }
    }

    public class CustomSaveDefiner : SaveableTypeDefiner {
        public CustomSaveDefiner() : base(156687423) { }

        protected override void DefineClassTypes() {
            AddClassDefinition(typeof(CultureData), 1);
            AddClassDefinition(typeof(CultureData.CultureFloat), 2);
        }

        protected override void DefineContainerDefinitions() {
            ConstructContainerDefinition(typeof(List<CultureData.CultureFloat>));
            ConstructContainerDefinition(typeof(Dictionary<Settlement, CultureData>));
            ConstructContainerDefinition(typeof(Dictionary<Clan, CultureData>));
            ConstructContainerDefinition(typeof(Dictionary<Kingdom, CultureData>));
        }
    }
}
