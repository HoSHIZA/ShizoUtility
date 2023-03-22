using System;
using UnityEngine;

namespace ShizoGames.Utilities
{
    /// <summary>
    /// Utility class for monitoring changes in screen resolution and orientation.
    /// </summary>
    public static class ScreenUtility
    {
        /// <summary>
        /// The amount of time (in seconds) between checks for changes in resolution or orientation.
        /// </summary>
        public static float PollingTime { get; private set; } = 0.2f;

        /// <summary>
        /// Event that is raised when the screen resolution changes.
        /// </summary>
        public static event Action<Resolution> ResolutionChanged;

        /// <summary>
        /// Event that is raised when the screen orientation changes.
        /// </summary>
        public static event Action<ScreenOrientation> OrientationChanged;

        /// <summary>
        /// The current screen orientation.
        /// </summary>
        public static ScreenOrientation CurrentOrientation => IsLandscape ? ScreenOrientation.Landscape : ScreenOrientation.Portrait;

        /// <summary>
        /// Returns true if the screen is in landscape mode, false otherwise.
        /// </summary>
        public static bool IsLandscape => Screen.width > Screen.height;

        /// <summary>
        /// Returns true if the screen is in portrait mode, false otherwise.
        /// </summary>
        public static bool IsPortrait => Screen.height > Screen.width;

        private static float _elapsedTime;
        private static Resolution _lastResolution;
        private static ScreenOrientation _lastOrientation;
        
        static ScreenUtility()
        {
            StaticUpdater.Add(UpdateEventType.Update, Update);
        }
        
        /// <summary>
        /// Sets the polling time for checking changes in resolution or orientation.
        /// </summary>
        /// <param name="value">The new polling time value (in seconds).</param>
        public static void SetPollingTime(float value)
        {
            _elapsedTime = 0;

            PollingTime = value < 0 ? 0 : value;
        }
        
        private static void Update()
        {
            _elapsedTime += Time.unscaledDeltaTime;

            if (!(_elapsedTime >= PollingTime)) return;

            _elapsedTime -= PollingTime;

            // Orientation
            {
                var orientation = IsPortrait ? ScreenOrientation.Portrait : ScreenOrientation.Landscape;

                if (orientation == _lastOrientation) return;

                _lastOrientation = orientation;

                OrientationChanged?.Invoke(orientation);
            }

            // Resolution
            {
                var resolution = Screen.currentResolution;

                if (resolution.ToString() == _lastResolution.ToString()) return;

                _lastResolution = resolution;

                ResolutionChanged?.Invoke(resolution);
            }
        }
        
        /// <summary>
        /// Enumeration of possible screen orientations.
        /// </summary>
        public enum ScreenOrientation
        {
            Landscape,
            Portrait,
        }
    }
}