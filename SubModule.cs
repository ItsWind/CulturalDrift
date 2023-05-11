using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace CulturalDrift {
    public class SubModule : MBSubModuleBase {
        public static Random Random = new();

        protected override void OnSubModuleLoad() {
            new Harmony("Windwhistle.CulturalDrift").PatchAll();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarter) {
            gameStarter.AddModel(new NewVolunteerModel());
            
            if (game.GameType is Campaign) {
                CampaignGameStarter campaignStarter = (CampaignGameStarter)gameStarter;

                campaignStarter.AddBehavior(new CulturalDriftBehavior());
            }
        }
    }
}