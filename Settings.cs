using UnityEngine;
using UnityModManagerNet;

using static UnityModManagerNet.UnityModManager;

namespace FASTER
{
    public class Settings : ModSettings, IDrawable
    {
        public enum DistortionType
        {
            In_Distortion,
            Out_Distortion
        }

        [Header("General")]
        [Draw(DrawType.Slider, Min = 0, Max = 150)]
        public int minSpeedThreshold = 80;
        [Draw(DrawType.Slider, Min = 50, Max = 200)]
        public int maxSpeedThreshold = 180;
        [Space]
        [Draw(DrawType.Slider, Min = 0.5f, Max = 7, Precision = 1)]
        public float effectUpSpeed = 3;
        [Draw(DrawType.Slider, Min = 1, Max = 7, Precision = 1)]
        public float effectDownSpeed = 4.5f;

        [Header("Lens distortion")]
        [Draw(DrawType.Toggle)]
        public bool enableLensDistortion = true;
        [Draw(DrawType.Slider, VisibleOn = "enableLensDistortion|true", Min = 30, Max = 90)]
        public int distortionIntensity = 40;

        [Header("Chromatic aberration")]
        [Draw(DrawType.Toggle)]
        public bool enableChromaticAberration = true;
        [Draw(DrawType.Toggle, VisibleOn = "enableChromaticAberration|true")]
        public bool aberrationFastMode = false;
        [Draw(DrawType.Slider, VisibleOn = "enableChromaticAberration|true", Min = 0.5f, Max = 3, Precision = 1)]
        public float aberrationIntensity = 1;

        [Header("Bloom")]
        [Draw(DrawType.Toggle)]
        public bool enableBloom = true;
        [Draw(DrawType.Slider, VisibleOn = "enableBloom|true", Min = 0.8f, Max = 0.2f, Precision = 1)]
        public float bloomThreshold = 0.4f;
        [Draw(DrawType.Slider, VisibleOn = "enableBloom|true", Min = 0.5f, Max = 5, Precision = 1)]
        public float bloomIntensity = 3f;

        [Header("Vignette")]
        [Draw(DrawType.Toggle)]
        public bool enableVignette = true;
        [Draw(DrawType.Slider, VisibleOn = "enableVignette|true", Min = 0.15f, Max = 0.5f, Precision = 1)]
        public float vignetteIntensity = 0.3f;

        [Header("Debug")]
        [Draw(DrawType.Toggle)]
        public bool testMaxEffect;
        [Draw(DrawType.Toggle)]
        public bool disableInfoLogs;

        public override void Save(ModEntry modEntry) => Save(this, modEntry);

        public void OnChange()
        {
            // TODO : Do validation for minSpeedThreshold and maxSpeedThreshold
        }
    }
}
