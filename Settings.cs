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
        [Draw(DrawType.PopupList, VisibleOn = "enableLensDistortion|true")]
        public DistortionType distortionType = DistortionType.In_Distortion;
        [Draw(DrawType.Slider, VisibleOn = "enableLensDistortion|true", Min = 0, Max = 40)]
        public int distortionIntensityIn = 30;
        [Draw(DrawType.Slider, VisibleOn = "enableLensDistortion|true", Min = 0, Max = 70)]
        public int distortionIntensityOut = 50;
        [Draw(DrawType.Toggle, VisibleOn = "enableLensDistortion|true")]
        public bool distortionFastMode = false;

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
