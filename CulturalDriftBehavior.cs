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
        public static CulturalDriftBehavior Instance;
        public Dictionary<Settlement, CultureData> SettlementCultureData = new();
        public Dictionary<Clan, CultureData> ClanCultureData = new();

        public CulturalDriftBehavior() {
            Instance = this;
        }

        public override void RegisterEvents() {
            CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, (settlement) => {
                if (settlement.Culture == null || settlement.IsHideout)
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
                // Settlements
                foreach (KeyValuePair<Settlement, CultureData> kvp in Instance.SettlementCultureData.ToList()) {
                    // FIX BUG
                    if (!Campaign.Current.Settlements.Contains(kvp.Key) || kvp.Key.IsHideout) {
                        Instance.SettlementCultureData.Remove(kvp.Key);
                        continue;
                    }

                    // Set settlement cultures, as they do not save/load on their own.
                    kvp.Key.Culture = kvp.Value.GetMainCulture();
                }
                // Clans
                foreach (KeyValuePair<Clan, CultureData> kvp in Instance.ClanCultureData.ToList()) {
                    // FIX BUG
                    if (!Campaign.Current.Clans.Contains(kvp.Key)) {
                        Instance.ClanCultureData.Remove(kvp.Key);
                        continue;
                    }
                }
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
