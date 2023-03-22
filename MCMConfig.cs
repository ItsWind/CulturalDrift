using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulturalDrift {
    internal sealed class MCMConfig : AttributeGlobalSettings<MCMConfig> {
        public override string Id => "CulturalDrift";
        public override string DisplayName => "CulturalDrift";
        public override string FolderName => "CulturalDrift";
        public override string FormatType => "xml";

        //War Change Variables//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [SettingPropertyFloatingInteger("Settlement Daily Culture Modifier", 0f, 100f, Order = 1, RequireRestart = false, HintText = "Control the amount of modifier to give a settlement culture daily. Culture floats go from 0-100")]
        [SettingPropertyGroup("Daily Modifiers")]
        public float SettlementDailyMod { get; set; } = 0.25f;
        //*********************************************************************************************************************************************************************************************************************************************************************************************************************
        [SettingPropertyFloatingInteger("Clan Daily Culture Modifier", 0f, 100f, Order = 2, RequireRestart = false, HintText = "Control the amount of modifier to give a clan culture daily. Culture floats go from 0-100")]
        [SettingPropertyGroup("Daily Modifiers")]
        public float ClanDailyMod { get; set; } = 0.25f;
        [SettingPropertyBool("Enable Birth Culture Modifier", HintText = "Enable this to make children born have a 50/50 chance of getting mother/father culture.", IsToggle = true, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup("Toggles")]
        public bool BirthCultureModifier { get; set; } = true;
        /*
        //*********************************************************************************************************************************************************************************************************************************************************************************************************************
        [SettingPropertyFloatingInteger("Kingdom Daily Culture Modifier", 0f, 100f, Order = 3, RequireRestart = false, HintText = "NOT YET IMPLEMENTED")]
        [SettingPropertyGroup("Daily Modifiers")]
        public float KingdomDailyMod { get; set; } = 0.25f;*/
    }
}
