using HarmonyLib;
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

    [HarmonyPatch(typeof(CarCameras))]
    static class SpeedEffectManager
    {
        static PostProcessProfile customProfile;

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void StartPostfix(CarCameras __instance)
        {
            if (!Main.enabled)
                return;

            PostProcessVolume volume = __instance.GetComponentInChildren<PostProcessVolume>();
            customProfile = PostProcessProfile.CreateInstance<PostProcessProfile>();

            foreach (PostProcessEffectSettings effectSettings in volume.profile.settings)
                customProfile.settings.Add(effectSettings);

            LensDistortion lensDistortion = customProfile.AddSettings<LensDistortion>();
            lensDistortion.intensity.overrideState = true;
            lensDistortion.centerY.overrideState = true;

            // TODO : Set lensDistortion.intensity default value

            ChromaticAberration chromaticAberration = customProfile.AddSettings<ChromaticAberration>();
            chromaticAberration.intensity.overrideState = true;
            chromaticAberration.fastMode.overrideState = true;

            // TODO : Set chromaticAberration.intensity default value
            // TODO : Set chromaticAberration.fastMode default value

            volume.profile = customProfile;
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void UpdatePostfix()
        {
            Main.Log("UpdatePostfix");

            // TODO : Set this (LensDistortion.centerY.value) on update
        }
    }
}