using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using System.Numerics;

namespace Identicons.ColorSchemes
{
    /// <summary>
    /// A color scheme that switches between four colors equally spaced around the color wheel.
    /// 
    /// Note that unlike <see cref="DivergentColorScheme"/>, it doesn't contain steps between
    /// those colors, so the resulting Identicon will have exactly four colors (or theoretically
    /// less, although that won't happen very often).
    /// </summary>
    public record TetradicColorScheme(float Hue) : IColorScheme
    {
        public Color GetColor(int index)
        {
            var hue = Hue + 90f * index;
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
