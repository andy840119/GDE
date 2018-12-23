using System;
using osuTK.Graphics;

namespace GDE.App.Main.Colours
{
    public class GDEColours
    {
        /// <summary>
        /// Returns a Color4 value from a Hex strinhgdefk;joiu 67n5jiyrt;l'fp
        /// </summary>
        /// <param name="hex">Hex value (pretty self explanitory)</param>
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
                        (byte)(Convert.ToByte(hex.Substring(0, 1), 16) * 17),
                        (byte)(Convert.ToByte(hex.Substring(1, 1), 16) * 17),
                        (byte)(Convert.ToByte(hex.Substring(2, 1), 16) * 17),
                        255);
                case 6:
                    return new Color4(
                        Convert.ToByte(hex.Substring(0, 2), 16),
                        Convert.ToByte(hex.Substring(2, 2), 16),
                        Convert.ToByte(hex.Substring(4, 2), 16),
                        255);
            }
        }

        // TODO: Add official colours here:
    }
}
