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

            switch (hex.Length)
            {
                default:
                    throw new ArgumentException(@"Invalid hex string length!");
                case 3:
                    return new Color4(
                        convertToHex(hex, 0, 2),
                        convertToHex(hex, 2, 2),
                        convertToHex(hex, 4, 2),
                    255);
                case 6:
                    return new Color4(
                        convertToHex(hex, 0, 1) * 17,
                        convertToHex(hex, 1, 1) * 17,
                        convertToHex(hex, 2, 1) * 17,
                    255);
            }

            Byte convertToHex(string _hex, int n, int k)
            {
                return Convert.ToByte(hex.Substring(n, k), 16);
            }
        }

        // TODO: Add official colours here:
    }
}
