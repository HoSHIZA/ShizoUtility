using System;
using System.Collections.Generic;
using System.Linq;

namespace ShizoGames.ShizoUtility
{
    /// <summary>
    /// Provides utility functions for working with different number types.
    /// </summary>
    public static class NumberTypesUtility
    {
        /// <summary>
        /// A collection of integer types.
        /// </summary>
        public static readonly Type[] Integer =
        {
            typeof(sbyte), typeof(byte), typeof(short),
            typeof(ushort), typeof(int), typeof(uint),
            typeof(long), typeof(ulong),
        };
        
        /// <summary>
        /// A collection of decimal types.
        /// </summary>
        public static readonly Type[] Decimal =
        {
            typeof(float), typeof(double),
        };
        
        /// <summary>
        /// A dictionary of value ranges for each supported number type.
        /// </summary>
        public static readonly IReadOnlyDictionary<Type, ValueRange> ValueRanges = new Dictionary<Type, ValueRange>
        {
            { typeof(sbyte), new ValueRange(sbyte.MinValue, sbyte.MaxValue) },
            { typeof(byte), new ValueRange(byte.MinValue, byte.MaxValue) },
            { typeof(short), new ValueRange(short.MinValue, short.MaxValue) },
            { typeof(ushort), new ValueRange(ushort.MinValue, ushort.MaxValue) },
            { typeof(int), new ValueRange(int.MinValue, int.MaxValue) },
            { typeof(uint), new ValueRange(uint.MinValue, uint.MaxValue) },
            { typeof(long), new ValueRange(long.MinValue, long.MaxValue) },
            { typeof(ulong), new ValueRange(ulong.MinValue, ulong.MaxValue) },
            { typeof(float), new ValueRange(float.MinValue, float.MaxValue) },
            { typeof(double), new ValueRange(double.MinValue, double.MaxValue) },
        };
        
        /// <summary>
        /// Clamps the given value within the specified range.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="type">The type of value determining the range.</param>
        /// <returns>The clamped value within the specified range.</returns>
        public static double ClampInRange(double value, Type type)
        {
            if (!ValueRanges.ContainsKey(type)) return 0;
            
            var range = ValueRanges[type];

            if (value < range.MinValue)
            {
                value = range.MinValue;
            }
            else if (value > range.MaxValue)
            {
                value = range.MaxValue;
            }

            return value;
        }
        
        /// <summary>
        /// Determines if the specified type is a number type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is a number type; otherwise, false.</returns>
        public static bool IsNumber(Type type) => IsInteger(type) || IsDecimal(type);
        
        /// <summary>
        /// Determines if the specified type is a number type.
        /// </summary>
        /// <typeparam name="T">The type to check.</typeparam>
        /// <returns>True if the type is a number type; otherwise, false.</returns>
        public static bool IsNumber<T>() => IsInteger<T>() || IsDecimal<T>();
        
        /// <summary>
        /// Determines if the specified type is an integer type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is an integer type; otherwise, false.</returns>
        public static bool IsInteger(Type type) => Integer.Any(t => t == type);
        
        /// <summary>
        /// Determines if the specified type is an integer type.
        /// </summary>
        /// <typeparam name="T">The type to check.</typeparam>
        /// <returns>True if the type is an integer type; otherwise, false.</returns>
        public static bool IsInteger<T>() => Integer.Any(t => t is T);
        
        /// <summary>
        /// Determines if the specified type is a decimal type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is a decimal type; otherwise, false.</returns>
        public static bool IsDecimal(Type type) => Decimal.Any(t => t == type);
        
        /// <summary>
        /// Determines if the specified type is a decimal type.
        /// </summary>
        /// <typeparam name="T">The type to check.</typeparam>
        /// <returns>True if the type is a decimal type; otherwise, false.</returns>
        public static bool IsDecimal<T>() => Decimal.Any(t => t is T);
        
        /// <summary>
        /// A struct representing the minimum and maximum values for a number type.
        /// </summary>
        public readonly struct ValueRange
        {
            /// <summary>
            /// The minimum value for the number type.
            /// </summary>
            public readonly double MinValue;
            
            /// <summary>
            /// The maximum value for the number type.
            /// </summary>
            public readonly double MaxValue;
            
            /// <summary>
            /// Creates a new ValueRange instance with the specified minimum and maximum values.
            /// </summary>
            /// <param name="minValue">The minimum value for the number type.</param>
            /// <param name="maxValue">The maximum value for the number type.</param>
            public ValueRange(double minValue, double maxValue)
            {
                MaxValue = maxValue;
                MinValue = minValue;
            }
        }
    }
}