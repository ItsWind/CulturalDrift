using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MCM.Abstractions.Base.Global;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace CulturalDrift.Patches {
    [HarmonyPatch(typeof(HeroCreator), nameof(HeroCreator.DeliverOffSpring))]
    internal class ConceiveChildPatch {
        [HarmonyPostfix]
        private static void Postfix(ref Hero __result) {
            if (!GlobalSettings<MCMConfig>.Instance.BirthCultureModifier)
                return;

            __result.Culture = (((double)MBRandom.RandomFloat < 0.5) ? __result.Father.Culture : __result.Mother.Culture);
        }
    }
}
