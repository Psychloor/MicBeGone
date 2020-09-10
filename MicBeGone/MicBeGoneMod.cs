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

    using UnityEngine;

    using VRC.Core;

    public sealed class MicBeGoneMod : MelonMod
    {

        private const string SettingsCategory = "MicBeGone";

        private static bool micEnabled;

        private HudVoiceIndicator hudVoiceIndicator;

        public override void OnApplicationStart()
        {
            MelonPrefs.RegisterCategory(SettingsCategory, "Mic Be-Gone");
            MelonPrefs.RegisterBool(SettingsCategory, "MicEnabled", micEnabled, "Mic Enabled");
            micEnabled = MelonPrefs.GetBool(SettingsCategory, "MicEnabled");
        }

        public override void VRChat_OnUiManagerInit()
        {
            hudVoiceIndicator = Object.FindObjectOfType<HudVoiceIndicator>();
            ApplySettings();
        }

        private void ApplySettings()
        {
            hudVoiceIndicator.enabled = micEnabled;
            if (micEnabled) return;
            hudVoiceIndicator.field_Private_Image_0.enabled = false;
            hudVoiceIndicator.field_Private_Image_1.enabled = false;
        }

        public override void OnModSettingsApplied()
        {
            micEnabled = MelonPrefs.GetBool(SettingsCategory, "MicEnabled");
            ApplySettings();
        }

    }

}