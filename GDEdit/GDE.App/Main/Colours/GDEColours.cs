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
                    return convertToHex3(hex);
                case 6:
                    return convertToHex6(hex);
                     
            }

            Color4 convertToHex6(string _hex)
            {
                return new Color4(
                    Convert.ToByte(_hex.Substring(0, 2), 16),
                    Convert.ToByte(_hex.Substring(2, 2), 16),
                    Convert.ToByte(_hex.Substring(4, 2), 16),
                    255);
            }

            Color4 convertToHex3(string _hex)
            {
                return new Color4(
                    (byte)(Convert.ToByte(_hex.Substring(0, 1), 16) * 17),
                    (byte)(Convert.ToByte(_hex.Substring(1, 1), 16) * 17),
                    (byte)(Convert.ToByte(_hex.Substring(2, 1), 16) * 17),
                    255);
            }
        }

        // TODO: Add official colours here:
    }
}
