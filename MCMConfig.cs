using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace CulturalDrift {
    internal sealed class MCMConfig : AttributeGlobalSettings<MCMConfig> {
        public override string Id => "CulturalDrift";
        public override string DisplayName => "CulturalDrift";
        public override string FolderName => "CulturalDrift";
        public override string FormatType => "xml";

        // DAILY MODIFIERS

        [SettingPropertyFloatingInteger("Clan Daily Culture Modifier", 0f, 100f, Order = 1, RequireRestart = false, HintText = "Control the amount of modifier to give a clan culture daily. Culture floats go from 0-100")]
        [SettingPropertyGroup("Daily Modifiers")]
        public float ClanDailyMod { get; set; } = 0.25f;

        [SettingPropertyFloatingInteger("Town/Village Daily Culture Modifier", 0f, 100f, Order = 2, RequireRestart = false, HintText = "Control the amount of modifier to give a town/village culture daily. Culture floats go from 0-100")]
        [SettingPropertyGroup("Daily Modifiers")]
        public float TownDailyMod { get; set; } = 0.25f;

        [SettingPropertyFloatingInteger("Castle Daily Culture Modifier", 0f, 100f, Order = 3, RequireRestart = false, HintText = "Control the amount of modifier to give a castle culture daily. Culture floats go from 0-100")]
        [SettingPropertyGroup("Daily Modifiers")]
        public float CastleDailyMod { get; set; } = 1f;

        // TROOP RECRUITMENT SPAWNING

        [SettingPropertyInteger("Settlement Culture Troops", 1, 100, Order = 1, RequireRestart = false, HintText = "Weight of a settlement spawning its culture's troops.")]
        [SettingPropertyGroup("Troop Spawning Weights")]
        public int SettlementCultureTroopSpawnWeight { get; set; } = 40;

        [SettingPropertyInteger("Default Culture Troops", 0, 100, Order = 2, RequireRestart = false, HintText = "Weight of a settlement spawning its starting culture's troops.")]
        [SettingPropertyGroup("Troop Spawning Weights")]
        public int DefaultCultureTroopSpawnWeight { get; set; } = 10;

        [SettingPropertyInteger("Owner Clan Culture Troops", 0, 100, Order = 3, RequireRestart = false, HintText = "Weight of a settlement spawning its owning clan culture's troops.")]
        [SettingPropertyGroup("Troop Spawning Weights")]
        public int OwnerClanCultureTroopSpawnWeight { get; set; } = 30;

        [SettingPropertyInteger("Kingdom Culture Troops", 0, 100, Order = 4, RequireRestart = false, HintText = "Weight of a settlement spawning its kingdom culture's troops.")]
        [SettingPropertyGroup("Troop Spawning Weights")]
        public int KingdomCultureTroopSpawnWeight { get; set; } = 20;

        // TOGGLES

        [SettingPropertyBool("Enable Birth Culture Modifier", HintText = "Enable this to make children born have a 50/50 chance of getting mother/father culture.", IsToggle = true, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup("Toggles")]
        public bool BirthCultureModifier { get; set; } = true;
    }
}
