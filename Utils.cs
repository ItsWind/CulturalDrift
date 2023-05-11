using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace CulturalDrift {
    public static class Utils {
        public static void PrintToMessages(string str, float r = 255, float g = 255, float b = 255) {
            float[] newValues = { r / 255.0f, g / 255.0f, b / 255.0f };
            Color col = new(newValues[0], newValues[1], newValues[2]);
            InformationManager.DisplayMessage(new InformationMessage(str, col));
        }

        public static bool IsCultureValid(CultureObject culture) {
            if (culture.EliteBasicTroop == null || culture.BasicTroop == null)
                return false;

            if (culture.BasicMercenaryTroops.Count == 0)
                return false;

            return true;
        }
    }
}
