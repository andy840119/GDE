using System;
using System.Collections.Generic;
using System.Text;

namespace GDEdit.Utilities.Attributes
{
    /// <summary>Contains the default value of the object parameter.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class DefaultParameterValueAttribute : Attribute
    {
        /// <summary>The default value of the parameter.</summary>
        public virtual object DefaultValue { get; }

        /// <summary>Initializes a new instance of the <seealso cref="DefaultParameterValueAttribute"/> attribute.</summary>
        /// <param name="defaultValue">The default value of the parameter.</param>
        public DefaultParameterValueAttribute(object defaultValue)
        {
            DefaultValue = defaultValue;
        }
    }
}
