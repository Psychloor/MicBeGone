using MelonLoader;

using MicBeGone;

using BuildInfo = MicBeGone.BuildInfo;

[assembly: MelonInfo(typeof(MicBeGoneMod), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author, BuildInfo.DownloadLink)]
[assembly: MelonGame("VRChat", "VRChat")]

namespace MicBeGone
{

    using System.Reflection;

    using Harmony;

    using MelonLoader;

    public class MicBeGoneMod : MelonMod
    {

        private const string SettingsCategory = "MicBeGone";

        private static bool micEnabled;

        public override void OnApplicationStart()
        {
            MelonPrefs.RegisterCategory(SettingsCategory, "Mic Be-Gone");
            MelonPrefs.RegisterBool(SettingsCategory, "MicEnabled", micEnabled, "Mic Enabled");
            micEnabled = MelonPrefs.GetBool(SettingsCategory, "MicEnabled");

            harmonyInstance.Patch(
                typeof(HudVoiceIndicator).GetMethod(nameof(HudVoiceIndicator.Update), BindingFlags.Public | BindingFlags.Instance),
                null,
                new HarmonyMethod(typeof(MicBeGoneMod).GetMethod(nameof(MicPatch), BindingFlags.NonPublic | BindingFlags.Static)));
        }

        public override void OnModSettingsApplied()
        {
            micEnabled = MelonPrefs.GetBool(SettingsCategory, "MicEnabled");
        }

        private static void MicPatch(ref HudVoiceIndicator __instance)
        {
            if (micEnabled) return;
            __instance.field_Private_Image_0.enabled = false;
            __instance.field_Private_Image_1.enabled = false;
        }

    }

}