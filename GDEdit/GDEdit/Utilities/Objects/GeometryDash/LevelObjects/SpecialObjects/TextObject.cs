using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.GeometryDash.LevelObjects.Triggers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;
using static System.Text.Encoding;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a text object.</summary>
    [ObjectID(SpecialObjectType.TextObject)]
    public class TextObject : ConstantIDSpecialObject
    {
        /// <summary>The object ID of the text object.</summary>
        public override int ObjectID => (int)SpecialObjectType.TextObject;

        /// <summary>Represents the Text property of the text object encoded in base 64.</summary>
        [ObjectStringMappable(ObjectParameter.TextObjectText)]
        public string Base64Text
        {
            get => ToBase64String(UTF8.GetBytes(Text));
            set => Text = ASCII.GetString(Base64Decrypt(value));
        }

        /// <summary>Represents the Text property of the text object.</summary>
        public string Text { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="TextObject"/> class.</summary>
        public TextObject() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="TextObject"/> class.</summary>
        /// <param name="x">The X location of the object.</param>
        /// <param name="y">The Y location of the object.</param>
        /// <param name="text">The text of the text object.</param>
        public TextObject(double x, double y, string text = "")
            : this()
        {
            X = x;
            Y = y;
            Text = text;
        }

        /// <summary>Returns a clone of this <seealso cref="TextObject"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new TextObject());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as TextObject;
            c.Text = Text;
            return base.AddClonedInstanceInformation(c);
        }

        private static byte[] Base64Decrypt(string encodedData)
        {
            while (encodedData.Length % 4 != 0)
                encodedData += "=";
            byte[] encodedDataAsBytes = FromBase64String(encodedData.Replace('-', '+').Replace('_', '/').Replace("\0", string.Empty));
            return encodedDataAsBytes;
        }
    }
}
