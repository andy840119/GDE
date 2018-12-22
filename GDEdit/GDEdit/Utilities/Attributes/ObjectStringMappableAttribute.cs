using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int Key;
    }
}
