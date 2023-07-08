#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

namespace ShizoGames.ShizoUtility
{
    /// <summary>
    /// A utility class for working with assets in Unity.
    /// </summary>
    /// <remarks>
    /// Only works in unity editor.
    /// </remarks>
    public static class AssetDatabaseUtility
    {
        /// <summary>
        /// Loads all assets with type T using AssetDatabase.
        /// </summary>
        /// <typeparam name="T">Type of assets to be loaded.</typeparam>
        /// <param name="type">Type of assets to be loaded.</param>
        /// <returns>An array of loaded assets.</returns>
        public static T[] LoadAllAssetsWithType<T>(Type type) where T : Object
        {
            var list = new List<T>();
            
            var guids = AssetDatabase.FindAssets($"t:{type}");
            
            foreach (var guid in guids)
            {
                T instance;
                
                try
                {
                    instance = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid));
                }
                catch
                {
                    continue;
                }
                
                if (instance == null) continue;
                
                list.Add(instance);
            }
            
            return list.ToArray();
        }
    }
}
#endif