using System;
using osuTK.Graphics;

namespace GDE.App.Main.Colours
{
    public class GDEColours
    {
        /// <summary>
        /// Returns a Color4 value from a Hex String
        /// </summary>
        /// <param name="hex">Hex value (pretty self explanatory)</param>
        /// <returns><see cref="Color4"/> from Hex</returns>
        public static Color4 FromHex(string hex)
        {
            if (hex[0] == '#')
                hex = hex.Substring(1);

            Console.WriteLine(hex);

            switch (hex.Length)
            {
                case 3:
                    return new Color4((byte)(getByte(0, 1) * 17), (byte)(getByte(1, 1) * 17), (byte)(getByte(2, 1) * 17), 255);
                case 6:
                    return new Color4(getByte(0, 2), getByte(2, 2), getByte(4, 2), 255);
                default:
                    throw new ArgumentException(@"Invalid hex string length!");
            }

            byte getByte(int n, int k) => Convert.ToByte(hex.Substring(n, k), 16);
        }

        // TODO: Add official colours here:
    }
}