using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace ShizoGames.Utilities
{
    /// <summary>
    /// A static class that allows registering and invoking update events for various update types.
    /// </summary>
    public static class StaticUpdater
    {
        private static readonly Dictionary<UpdateEventType, List<Action>> _events = new Dictionary<UpdateEventType, List<Action>>();
        
        private static bool _isSetup;
        
        static StaticUpdater()
        {
            foreach (UpdateEventType type in Enum.GetValues(typeof(UpdateEventType)))
            {
                _events[type] = new List<Action>(5);
            }
        }
        
        /// <summary>
        /// Adds an update event to the specified update type.
        /// </summary>
        /// <param name="updateEventType">The type of update to register the event for.</param>
        /// <param name="action">The action to invoke during the update event.</param>
        public static void Add(UpdateEventType updateEventType, Action action)
        {
            if (action == null) return;

            _events[updateEventType].Add(action);
        }
        
        /// <summary>
        /// Removes the specified update event from the specified update type.
        /// </summary>
        /// <param name="updateEventType">The type of update to remove the event from.</param>
        /// <param name="action">The action to remove from the update event.</param>
        public static void Remove(UpdateEventType updateEventType, Action action)
        {
            if (action == null) return;

            _events[updateEventType].Remove(action);
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Setup()
        {
            if (_isSetup) return;

            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();

            foreach (UpdateEventType type in Enum.GetValues(typeof(UpdateEventType)))
            {
                var subsystemType = GetTypeFromUpdateEventType(type);

                if (subsystemType == null) continue;

                for (var i = 0; i < playerLoop.subSystemList.Length; i++)
                {
                    if (playerLoop.subSystemList[i].type == subsystemType)
                    {
                        playerLoop.subSystemList[i].updateDelegate += () => InvokeEvents(_events[type]);

                        break;
                    }
                }
            }

            PlayerLoop.SetPlayerLoop(playerLoop);

            _isSetup = true;
        }

        private static Type GetTypeFromUpdateEventType(UpdateEventType type)
        {
            switch (type)
            {
                case UpdateEventType.EarlyUpdate:
                    return typeof(EarlyUpdate);
                case UpdateEventType.FixedUpdate:
                    return typeof(FixedUpdate);
                case UpdateEventType.PreUpdate:
                    return typeof(PreUpdate);
                case UpdateEventType.Update:
                    return typeof(Update);
                case UpdateEventType.PreLateUpdate:
                    return typeof(PreLateUpdate);
                case UpdateEventType.PostLateUpdate:
                    return typeof(PostLateUpdate);
                default:
                    return null;
            }
        }

        private static void InvokeEvents(IReadOnlyList<Action> events)
        {
            for (var i = 0; i < events.Count; i++)
            {
                events[i].Invoke();
            }
        }
    }

    /// <summary>
    /// The types of update events that can be registered with StaticUpdater.
    /// </summary>
    public enum UpdateEventType
    {
        EarlyUpdate = 2,
        FixedUpdate = 3,
        PreUpdate = 4,
        Update = 5,
        PreLateUpdate = 6,
        PostLateUpdate = 7,
    }
}