using UnityEngine;

namespace Brinkshift.Gameplay
{
    /// <summary>
    /// Gate 0 frame pacing.
    ///
    /// iOS starts new apps at 30 fps and caps them at 60 Hz unless ProMotion is
    /// enabled (Player Settings > iOS > "Enable ProMotion", i.e.
    /// <c>appleEnableProMotion</c>). This makes the game present frames at the
    /// device's real refresh rate - 120 Hz on ProMotion iPhones - so the player
    /// stops looking choppy while dragging.
    ///
    /// This ONLY changes how often frames are shown. It adds no input smoothing,
    /// interpolation, easing, or latency. The relative-drag controller stays
    /// delta-based and frame-rate independent.
    /// </summary>
    public static class FrameRateBootstrap
    {
        /// <summary>Never target below this - covers 60 Hz panels and bad refresh reads.</summary>
        private const int MinTargetFps = 60;

        /// <summary>Editor / desktop cap: smooth, without pinning a core on invisible frames.</summary>
        private const int DesktopTargetFps = 120;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Apply()
        {
            // Application.targetFrameRate is only honoured when vSync is off, and
            // the quality levels ship with mixed vSyncCount values.
            QualitySettings.vSyncCount = 0;

#if UNITY_IOS || UNITY_ANDROID
            Application.targetFrameRate = Mathf.Max(MinTargetFps, ReadDisplayRefreshRate());
#else
            Application.targetFrameRate = DesktopTargetFps;
#endif
        }

        private static int ReadDisplayRefreshRate()
        {
            double hz = Screen.currentResolution.refreshRateRatio.value;
            return hz > 1.0 ? Mathf.RoundToInt((float)hz) : MinTargetFps;
        }
    }
}
