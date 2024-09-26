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
        [Draw(DrawType.Slider, VisibleOn = "enableLensDistortion|true", Min = 0, Max = 40)]
        public int distortionIntensity = 30;

        // TODO : This should be chromatic aberration
        //[Draw(DrawType.Toggle, VisibleOn = "enableLensDistortion|true")]
        //public bool distortionFastMode = false;

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
