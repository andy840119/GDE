using osu.Framework.Graphics.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Convert;
using static System.Char;
using osu.Framework.Bindables;

namespace GDE.App.Main.UI
{
    public class NumberTextBox : TextBox
    {
        /// <summary>Gets the current numeric value in the textbox.</summary>
        public int Number => Current.Value.Length > 0 ? ToInt32(Current.Value) : 0;

        public event Action<int> NumberChanged;

        public NumberTextBox() : this(0) { }
        public NumberTextBox(int startingValue)
            : base()
        {
            Text = startingValue.ToString();
            Current.ValueChanged += OnNumberChanged;
        }

        protected override bool CanAddCharacter(char character) => IsNumber(character);

        private void OnNumberChanged(ValueChangedEvent<string> v) => NumberChanged?.Invoke(ToInt32(v.NewValue));
    }
}
