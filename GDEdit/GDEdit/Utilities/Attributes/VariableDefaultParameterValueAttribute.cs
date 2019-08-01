using System;
using System.Collections.Generic;
using System.Text;

namespace GDEdit.Utilities.Attributes
{
    /// <summary>Marks this field or property as an object parameter whose actual default value is variable across different object types, where a value indicates that the default value should be used.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class VariableDefaultParameterValueAttribute : DefaultParameterValueAttribute
    {
        /// <summary>Initializes a new instance of the <seealso cref="VariableDefaultParameterValueAttribute"/> attribute with a default value of 0.</summary>
        public VariableDefaultParameterValueAttribute() : base(0) { }
        /// <summary>Initializes a new instance of the <seealso cref="VariableDefaultParameterValueAttribute"/> attribute.</summary>
        /// <param name="defaultValue">The default value of the parameter.</param>
        public VariableDefaultParameterValueAttribute(object defaultValue) : base(defaultValue) { }
    }
}
