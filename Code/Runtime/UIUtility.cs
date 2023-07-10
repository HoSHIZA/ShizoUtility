using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace KDebugger.Plugins.ShizoGames.ShizoUtility
{
    /// <summary>
    /// Utilities for interacting with the user interface.
    /// </summary>
    public static class UIUtility
    {
        /// <summary>
        /// Creates a UI blocker that covers a given canvas content.
        /// </summary>
        /// <param name="rootCanvas">The root canvas.</param>
        /// <param name="content">The content to be blocked.</param>
        /// <param name="onClickBlocker">The action to be executed when the blocker is clicked. Default is null.</param>
        /// <param name="color">The color of the blocker. Default is transparent.</param>
        /// <param name="colorTransitionTime">The duration of the color transition. Default is 0 seconds.</param>
        /// <returns>The created blocker object.</returns>
        public static Blocker CreateBlocker(Canvas rootCanvas, RectTransform content, 
            Action onClickBlocker = null, Color color = default, float colorTransitionTime = 0f)
        {
            var blocker = new GameObject("Blocker");
            
            var blockerRectTransform = blocker.AddComponent<RectTransform>();
            blockerRectTransform.SetParent(rootCanvas.transform, false);
            blockerRectTransform.anchorMin = new Vector2(0, 0);
            blockerRectTransform.anchorMax = new Vector2(1, 1);
            blockerRectTransform.sizeDelta = Vector2.zero;
            
            var blockerCanvas = blocker.AddComponent<Canvas>();
            var contentCanvas = content.GetComponent<Canvas>();
            blockerCanvas.overrideSorting = true;
            blockerCanvas.sortingLayerID = contentCanvas.sortingLayerID;
            blockerCanvas.sortingOrder = contentCanvas.sortingOrder - 1;
            
            var parentCanvas = content.parent.GetComponentInParent<Canvas>();
            if (parentCanvas)
            {
                var components = parentCanvas.GetComponents<BaseRaycaster>();
                for (var i = 0; i < components.Length; i++)
                {
                    if (!blocker.TryGetComponent(components[i].GetType(), out _))
                    {
                        blocker.AddComponent(components[i].GetType());
                    }
                }
            }
            else
            {
                if (!blocker.TryGetComponent(out GraphicRaycaster _))
                {
                    blocker.AddComponent<GraphicRaycaster>();
                }
            }
            
            var blockerImage = blocker.AddComponent<Image>();
            blockerImage.color = Color.clear;
            if (color != Color.clear)
            {
                if (colorTransitionTime > 0)
                {
                    blockerImage.StartCoroutine(FadeColor(blockerImage, color, colorTransitionTime));
                }
                else
                {
                    blockerImage.color = color;
                }
            }
            
            if (onClickBlocker != null)
            {
                var blockerButton = blocker.AddComponent<Button>();
                blockerButton.onClick.AddListener(onClickBlocker.Invoke);
            }
            
            return new Blocker(blocker);
        }

        /// <summary>
        /// Destroys the given UI blocker.
        /// </summary>
        /// <param name="blocker">The blocker object to be destroyed.</param>
        /// <param name="colorTransitionTime">The duration of the color transition. Default is 0 seconds.</param>
        public static void DestroyBlocker(Blocker blocker, float colorTransitionTime = 0)
        {
            if (blocker == null || !blocker.Object) return;

            if (colorTransitionTime > 0)
            {
                if (blocker.Object.TryGetComponent<Image>(out var image))
                {
                    image.StartCoroutine(FadeColor(image, Color.clear, colorTransitionTime));
                    
                    Object.Destroy(blocker.Object, colorTransitionTime);
                }
            }
            else
            {
                Object.Destroy(blocker.Object);
            }
        }
        
        private static IEnumerator FadeColor(this Graphic graphic, Color targetColor, float duration)
        {
            var startColor = graphic.color;
            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                var t = elapsedTime / duration;
                graphic.color = Color.Lerp(startColor, targetColor, t);
                elapsedTime += Time.unscaledDeltaTime;

                yield return null;
            }

            graphic.color = targetColor;
        }
        
        /// <summary>
        /// Represents the UI blocker object.
        /// </summary>
        public sealed class Blocker
        {
            /// <summary>
            /// The game object of the blocker.
            /// </summary>
            public GameObject Object { get; }

            /// <summary>
            /// Initializes a new instance of the Blocker class with the given game object.
            /// </summary>
            /// <param name="obj">The game object of the blocker.</param>
            public Blocker(GameObject obj)
            {
                Object = obj;
            }
        }
    }
}