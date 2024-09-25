using UnityEngine;
using UnityModManagerNet;

using static UnityModManagerNet.UnityModManager;

namespace FASTER
{
    public class Settings : ModSettings, IDrawable
    {
        // [Draw(DrawType.)]

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
        }
    }
}
