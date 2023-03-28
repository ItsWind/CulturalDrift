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

        // TOGGLES

        [SettingPropertyBool("Enable Birth Culture Modifier", HintText = "Enable this to make children born have a 50/50 chance of getting mother/father culture.", IsToggle = true, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup("Toggles")]
        public bool BirthCultureModifier { get; set; } = true;
    }
}
