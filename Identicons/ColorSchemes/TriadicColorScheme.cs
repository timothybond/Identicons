using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using System.Numerics;

namespace Identicons.ColorSchemes
{
    /// <summary>
    /// A color scheme that switches between three colors equally spaced around the color wheel.
    /// 
    /// Note that unlike <see cref="DivergentColorScheme"/>, it doesn't contain steps between
    /// those colors, so the resulting Identicon will have exactly three colors (or theoretically
    /// less, although that won't happen very often).
    /// </summary>
    public record TriadicColorScheme(float Hue) : IColorScheme
    {
        public Color GetColor(int index)
        {
            // Note: because "index" should be 0-7, two of the three
            // colors will be represented more often than the third.
            // Although this is fine.
            var hue = Hue + 120f * index;
            while (hue > 360f)
            {
                hue -= 360f;
            }

            var hsl = new Hsl(hue, 1f, 0.5f);

            var rgb = ColorSpaceConverter.ToRgb(hsl);

            return new Color(new Vector4(rgb.R, rgb.G, rgb.B, 1f));
        }
    }
}
