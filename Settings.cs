using UnityEngine;
using UnityModManagerNet;

using static UnityModManagerNet.UnityModManager;

namespace FASTER
{
    public class Settings : ModSettings, IDrawable
    {
        // [Draw(DrawType.)]

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
        [Draw(DrawType.Slider, VisibleOn = "enableChromaticAberration|true", Min = 0.5f, Max = 2, Precision = 1)]
        public float aberrationIntensity = 1;

        //[Draw(DrawType.Toggle)]
        public bool showMarkers;

        [Header("Debug")]
        [Draw(DrawType.Toggle)]
        public bool testMaxEffect;
        [Draw(DrawType.Toggle)]
        public bool disableInfoLogs;

        public override void Save(ModEntry modEntry) => Save(this, modEntry);

        public void OnChange()
        {
            Main.SetMarkers(showMarkers);

            //

            // TODO : Do validation for minSpeedThreshold and maxSpeedThreshold
        }
    }
}
