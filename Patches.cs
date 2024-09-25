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

    // TODO : Add effects on profile
    // TODO : Replace PP profile
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

            //
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void UpdatePostfix()
        {
            Main.Log("UpdatePostfix");
        }
    }
}