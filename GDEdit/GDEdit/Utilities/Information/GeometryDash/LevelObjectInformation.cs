using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.General;

namespace GDEdit.Utilities.Information.GeometryDash
{
    /// <summary>Contains information about level object properties.</summary>
    public static class LevelObjectInformation
    {
        /// <summary>The highest parameter ID currently in the game.</summary>
        public const int ParameterCount = 118;

        // TODO: Deprecate
        /// <summary>The property IDs which represent an <seealso cref="int"/> value.</summary>
        public static readonly int[] IntParameters = { 1, 7, 8, 9, 12, 20, 21, 22, 23, 24, 25, 30, 48, 50, 51, 52, 61, 68, 69, 71, 76, 77, 79, 80, 81, 82, 88, 95, 97, 101, 108, 109, 115 };
        /// <summary>The property IDs which represent a <seealso cref="double"/> value.</summary>
        public static readonly int[] DoubleParameters = { 2, 3, 6, 10, 28, 29, 32, 35, 45, 46, 47, 54, 60, 63, 72, 73, 75, 84, 85, 90, 91, 92, 105, 107 };
        /// <summary>The property IDs which represent a <seealso cref="bool"/> value.</summary>
        public static readonly int[] BoolParameters = { 4, 5, 11, 13, 14, 15, 16, 17, 34, 36, 41, 42, 56, 58, 59, 62, 64, 65, 66, 67, 70, 78, 86, 87, 89, 93, 94, 96, 98, 99, 100, 102, 103, 106, 110, 116, 117, 118 };

        /// <summary>The property IDs which represent a <seealso cref="string"/> value.</summary>
        public static readonly int[] StringParameters = { 31 };
        /// <summary>The property IDs which represent an <seealso cref="int"/>[] value.</summary>
        public static readonly int[] IntArrayParameters = { 57 };
        /// <summary>The property IDs which represent an <seealso cref="HSVAdjustment"/> value.</summary>
        public static readonly int[] HSVParameters = { 43, 44, 49 };

        private static readonly ObjectParameterTypeAttribute[] objectParameterAttributes = new ObjectParameterTypeAttribute[ParameterCount];
        private static readonly Type[] objectParameterAttributeTypes = new Type[ParameterCount];

        static LevelObjectInformation()
        {
            var type = typeof(ObjectParameter);
            var baseAttributeType = typeof(ObjectParameterTypeAttribute);
            var attributeTypes = baseAttributeType.Assembly.GetTypes().Where(t => t.BaseType == baseAttributeType);
            foreach (var n in Enum.GetNames(type))
            {
                var m = type.GetMember(n).FirstOrDefault();
                ObjectParameterTypeAttribute a = null;
                for (int i = 0; i < attributeTypes.Count() && a == null; i++)
                    a = m.GetCustomAttributes(baseAttributeType, false).FirstOrDefault() as ObjectParameterTypeAttribute;
                int value = (int)Enum.Parse(type, n);
                if (value > 0)
                {
                    objectParameterAttributes[value - 1] = a;
                    objectParameterAttributeTypes[value - 1] = a?.GetType();
                }
            }
        }

        /// <summary>Returns the <seealso cref="Type"/> of the attribute of the chosen parameter ID in the <seealso cref="ObjectParameter"/> enum.</summary>
        /// <param name="parameterID">The parameter ID to get the string type of.</param>
        public static ObjectParameterTypeAttribute GetParameterIDAttribute(int parameterID) => objectParameterAttributes[parameterID - 1];
        /// <summary>Returns the <seealso cref="Type"/> of the attribute of the chosen parameter ID in the <seealso cref="ObjectParameter"/> enum.</summary>
        /// <param name="parameterID">The parameter ID to get the string type of.</param>
        public static Type GetParameterIDAttributeType(int parameterID) => objectParameterAttributeTypes[parameterID - 1];
        /// <summary>Returns the <seealso cref="Type"/> of the chosen parameter ID in the <seealso cref="ObjectParameter"/> enum.</summary>
        /// <param name="parameterID">The parameter ID to get the string type of.</param>
        public static Type GetParameterIDType(int parameterID) => objectParameterAttributes[parameterID - 1].Type;
    }
}
