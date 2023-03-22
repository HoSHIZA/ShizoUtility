using UnityEngine;

namespace ShizoGames.Utilities
{
    /// <summary>
    /// A utility class for working with colors in Unity.
    /// </summary>
    public static class ExtraColorUtility
    {
        private const float MAX_HUE = 360f;
        
        /// <summary>
        /// Generates a rainbow color by linearly interpolating across the full hue range.
        /// </summary>
        /// <param name="t">The interpolation value, between 0 and 1.</param>
        /// <returns>A color with a hue based on the interpolation value.</returns>
        public static Color RainbowLerp(float t)
        {
            var hue = Mathf.Lerp(0f, MAX_HUE, t);

            return Color.HSVToRGB(hue / MAX_HUE, 1f, 1f);
        }
        
        /// <summary>
        /// Generates a rainbow color by linearly interpolating between two hue values.
        /// </summary>
        /// <param name="start">The starting hue value, between 0 and 360.</param>
        /// <param name="end">The ending hue value, between 0 and 360.</param>
        /// <param name="t">The interpolation value, between 0 and 1.</param>
        /// <returns>A color with a hue based on the interpolation value between start and end.</returns>
        public static Color RainbowLerp(float start, float end, float t)
        {
            start = Mathf.Clamp(start, 0, MAX_HUE);
            end = Mathf.Clamp(end, 0, MAX_HUE);

            var hue = Mathf.Lerp(start, end, t);

            return Color.HSVToRGB(hue / MAX_HUE, 1f, 1f);
        }
    }
}