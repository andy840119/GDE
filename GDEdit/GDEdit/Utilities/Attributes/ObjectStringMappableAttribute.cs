using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations.GeometryDash.GamesaveValues;

namespace GDEdit.Utilities.Attributes
{
    /// <summary>Marks the property or field as a mappable element in the object string with the corresponding key.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class ObjectStringMappableAttribute : Attribute
    {
        public ObjectStringMappableAttribute(int key)
        {
            Key = key;
        }
        public ObjectStringMappableAttribute(ObjectParameter key)
        {
            Key = (int)key;
        }

        public int Key;
    }
}