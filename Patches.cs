using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace FASTER
{
    // Patch model
    // [HarmonyPatch(typeof(), nameof())]
    // [HarmonyPatch(typeof(), MethodType.)]
    // static class type_method_Patch
    // {
    // 	static void Prefix()
    // 	{
    // 		//
    // 	}

    // 	static void Postfix()
    // 	{
    // 		//
    // 	}
    // }

    // TODO : Add Bloom effect
    // TODO : Add Vignette effect

    [HarmonyPatch(typeof(HudManager), "Awake")]
    static class HudGetter
    {
        public static HudManager hud;

        static void Postfix(HudManager __instance) => hud = __instance;
    }

    [HarmonyPatch(typeof(CarCameras))]
    static class SpeedEffectManager
    {
        static PostProcessProfile customProfile;
        static Func<float> GetSpeed;
        static Transform player;

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void StartPostfix(CarCameras __instance)
        {
            if (HudGetter.hud != null)
                GetSpeed = () => Main.GetField<float, HudManager>(HudGetter.hud, "digitalSpeedoVelo", BindingFlags.Instance);

            PostProcessVolume volume = __instance.GetComponentInChildren<PostProcessVolume>();
            customProfile = PostProcessProfile.CreateInstance<PostProcessProfile>();
            customProfile.name = "Custom profile";

            foreach (PostProcessEffectSettings effectSettings in volume.profile.settings)
                customProfile.settings.Add(effectSettings);

            LensDistortion lensDistortion = customProfile.AddSettings<LensDistortion>();
            lensDistortion.enabled.overrideState = true;
            lensDistortion.enabled.value = Main.settings.enableLensDistortion;
            lensDistortion.intensity.overrideState = true;
            lensDistortion.centerY.overrideState = true;
            lensDistortion.intensity.value = 0;

            ChromaticAberration chromaticAberration = customProfile.AddSettings<ChromaticAberration>();
            chromaticAberration.enabled.overrideState = true;
            chromaticAberration.enabled.value = Main.settings.enableChromaticAberration;
            chromaticAberration.intensity.overrideState = true;
            chromaticAberration.fastMode.overrideState = true;
            chromaticAberration.fastMode.value = Main.settings.aberrationFastMode;

            volume.profile = customProfile;

            if (!Main.settings.disableInfoLogs)
                Main.Log("Switched post processing to " + volume.name);
        }

        [HarmonyPatch("LateUpdate")]
        [HarmonyPrefix]
        static void LateUpdatePrefix()
        {
            // also check here to make sure we have it
            if (GetSpeed == null && HudGetter.hud != null)
                GetSpeed = () => Main.GetField<float, HudManager>(HudGetter.hud, "digitalSpeedoVelo", BindingFlags.Instance);

            // don't move that to Awake, we risk a game crashing null ref
            if (player == null)
                player = GameEntryPoint.EventManager.playerManager.playerRigidBody.transform;

            if (GetSpeed == null || player == null)
                return;

            float speedPercent = Mathf.InverseLerp(Main.settings.minSpeedThreshold, Main.settings.maxSpeedThreshold, GetSpeed());

            if (Main.settings.testMaxEffect)
                speedPercent = 1;

            if (customProfile.TryGetSettings<LensDistortion>(out LensDistortion lens))
            {
                lens.enabled.value = Main.enabled && Main.settings.enableLensDistortion;

                if (lens.enabled.value)
                {
                    float positionPercent = Camera.main.WorldToViewportPoint(player.position + player.forward * 100).y;
                    lens.centerY.value = Mathf.Lerp(-0.5f, 0.5f, positionPercent);

                    lens.intensity.value = Mathf.Lerp(0, -Main.settings.distortionIntensity, speedPercent);
                }
            }

            if (customProfile.TryGetSettings<ChromaticAberration>(out ChromaticAberration aberration))
            {
                aberration.enabled.value = Main.enabled && Main.settings.enableChromaticAberration;

                if (aberration.enabled.value)
                {
                    aberration.fastMode.value = Main.settings.aberrationFastMode;
                    aberration.intensity.value = Mathf.Lerp(0, Main.settings.aberrationIntensity, speedPercent);
                }
            }
        }
    }
}