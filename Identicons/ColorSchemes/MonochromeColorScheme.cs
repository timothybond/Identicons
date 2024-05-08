using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using System.Numerics;

namespace Identicons.ColorSchemes
{
    public record MonochromeColorScheme(float Hue) : IColorScheme
    {
        public Color GetColor(int index)
        {
            index = 8 - index % 8;

            var hsl = new Hsl(Hue, 1f, 1f - index * 0.1f);

            var rgb = ColorSpaceConverter.ToRgb(hsl);

            return new Color(new Vector4(rgb.R, rgb.G, rgb.B, 1f));
        }
    }
}
