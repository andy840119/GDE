using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDEdit.Utilities.Enumerations.GeometryDash;

namespace GDEdit.Utilities.Attributes
{
    /// <summary>Marks the property or field as a mappable element in the guideline string with the corresponding key.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class GuidelineStringMappableAttribute : Attribute
    {
        /// <summary>The key the property represents in the guideline string.</summary>
        public string Key { get; }

        /// <summary>Initializes a new instance of the <seealso cref="GuidelineStringMappableAttribute"/> class.</summary>
        /// <param name="key">The key the property represents in the guideline string.</param>
        public GuidelineStringMappableAttribute(string key)
        {
            Key = key;
        }
    }
}