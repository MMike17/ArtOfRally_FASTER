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
        [Draw("Min speed threshold", DrawType.Slider, Min = 0, Max = 150)]
        public int minSpeedThreshold = 80;
        [Draw("Max speed threshold", DrawType.Slider, Min = 50, Max = 200)]
        public int maxSpeedThreshold = 180;
        [Space]
        [Draw("Effect up speed", DrawType.Slider, Min = 0.5f, Max = 7, Precision = 1)]
        public float effectUpSpeed = 3;
        [Draw("Effect down speed", DrawType.Slider, Min = 1, Max = 7, Precision = 1)]
        public float effectDownSpeed = 4.5f;
        [Space]
        [Draw("Reset values", DrawType.Toggle)]
        public bool resetValues = false;

        [Header("Lens distortion")]
        [Draw("Enable lens distortion", DrawType.Toggle)]
        public bool enableLensDistortion = true;
        [Draw("Distortion intensity", DrawType.Slider, VisibleOn = "enableLensDistortion|true", Min = 30, Max = 90)]
        public int distortionIntensity = 40;

        [Header("Chromatic aberration")]
        [Draw("Enable chromatic aberration", DrawType.Toggle)]
        public bool enableChromaticAberration = true;
        [Draw("Aberration fast mode", DrawType.Toggle, VisibleOn = "enableChromaticAberration|true")]
        public bool aberrationFastMode = false;
        [Draw("Aberration intensity", DrawType.Slider, VisibleOn = "enableChromaticAberration|true", Min = 0.5f, Max = 3, Precision = 1)]
        public float aberrationIntensity = 1;

        [Header("Bloom")]
        [Draw("Enable bloom", DrawType.Toggle)]
        public bool enableBloom = true;
        [Draw("Bloom threshold", DrawType.Slider, VisibleOn = "enableBloom|true", Min = 0.8f, Max = 0.2f, Precision = 1)]
        public float bloomThreshold = 0.4f;
        [Draw("Bloom Intensity", DrawType.Slider, VisibleOn = "enableBloom|true", Min = 0.5f, Max = 5, Precision = 1)]
        public float bloomIntensity = 3f;

        [Header("Vignette")]
        [Draw("Enable vignette", DrawType.Toggle)]
        public bool enableVignette = true;
        [Draw("Vignette intensity", DrawType.Slider, VisibleOn = "enableVignette|true", Min = 0.15f, Max = 0.5f, Precision = 1)]
        public float vignetteIntensity = 0.3f;

        [Header("Debug")]
        [Draw("Test max effect", DrawType.Toggle)]
        public bool testMaxEffect = false;
        [Draw("Disable info logs", DrawType.Toggle)]
        public bool disableInfoLogs = true;

        public override void Save(ModEntry modEntry) => Save(this, modEntry);

        public void OnChange()
        {
            // TODO : Do validation for minSpeedThreshold and maxSpeedThreshold

            if (resetValues)
            {
                // TODO : Reset values to default values
                resetValues = false;
            }
        }
    }
}
