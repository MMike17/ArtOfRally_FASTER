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

    // TODO : Set effect from car speed on update

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

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void StartPostfix(CarCameras __instance)
        {
            if (!Main.enabled)
                return;

            if (HudGetter.hud != null)
                GetSpeed = () => Main.GetField<float, HudManager>(HudGetter.hud, "digitalSpeedoVelo", BindingFlags.Instance);

            PostProcessVolume volume = __instance.GetComponentInChildren<PostProcessVolume>();
            customProfile = PostProcessProfile.CreateInstance<PostProcessProfile>();

            foreach (PostProcessEffectSettings effectSettings in volume.profile.settings)
                customProfile.settings.Add(effectSettings);

            LensDistortion lensDistortion = customProfile.AddSettings<LensDistortion>();
            lensDistortion.intensity.overrideState = true;
            lensDistortion.centerY.overrideState = true;
            lensDistortion.intensity.value = 0;

            ChromaticAberration chromaticAberration = customProfile.AddSettings<ChromaticAberration>();
            chromaticAberration.intensity.overrideState = true;
            chromaticAberration.fastMode.overrideState = true;
            chromaticAberration.fastMode.value = Main.settings.distortionFastMode;

            volume.profile = customProfile;
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void UpdatePostfix()
        {
            if (GetSpeed == null && HudGetter.hud != null)
                GetSpeed = () => Main.GetField<float, HudManager>(HudGetter.hud, "digitalSpeedoVelo", BindingFlags.Instance);

            if (customProfile.TryGetSettings<LensDistortion>(out LensDistortion lens))
            {
                lens.enabled.value = Main.settings.enableLensDistortion;

                if (Main.settings.enableLensDistortion)
                {
                    // TODO : Set this (LensDistortion.centerY.value) on update
                    //lens.centerY.value = ;

                    //float max
                    //float speedPercent = Mathf.InverseLerp();

                    //lens.intensity.value =
                    //    Main.settings.distortionType == Settings.DistortionType.In_Distortion ?
                    //    Main.settings.distortionIntensityIn :
                    //    Main.settings.distortionIntensityOut;
                }
            }
        }
    }
}