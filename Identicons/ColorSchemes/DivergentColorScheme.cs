using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using System.Numerics;

namespace Identicons.ColorSchemes
{
    /// <summary>
    /// A color scheme that moves between two colors on opposite ends of the color wheel.
    /// </summary>
    public record DivergentColorScheme(float Hue) : IColorScheme
    {
        public Color GetColor(int index)
        {
            index = index % 8;

            var oppositeHue = Hue + 180f;
            if (oppositeHue > 360f)
            {
                oppositeHue -= 360f;
            }

            Hsl hsl;

            if (index < 4)
            {
                hsl = new Hsl(Hue, 1f, 0.5f + 0.125f * index);
            }
            else
            {
                hsl = new Hsl(oppositeHue, 1f, 0.5f + 0.125f * (7 - index));
            }

            var rgb = ColorSpaceConverter.ToRgb(hsl);

            return new Color(new Vector4(rgb.R, rgb.G, rgb.B, 1f));
        }
    }
}
