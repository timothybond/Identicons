using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Identicons.Generators
{
    /// <summary>
    /// A generator that fills in the top half of an image,
    /// then mirrors it vertically across the midline.
    /// </summary>
    public class VerticalMirrorGenerator : BaseIdenticonGenerator
    {
        protected override Image GenerateImage(ulong hash, IColorScheme colorScheme)
        {
            // Note that this gets returned, so it shouldn't be disposed here,
            // but will be disposed in BaseIdenticonGenerator.Generate(...).
            Image image = new Image<Rgba32>(8, 8);
            using Image topHalf = new Image<Rgba32>(8, 4);

            var brushes = GetBrushes(colorScheme);

            var rhash = Hash.Reverse(hash);

            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 4; y++)
                {
                    var pixelIndex = y * 8 + x; // Max 31
                    Brush brush = brushes[GetBrushIndex(hash, pixelIndex)];

                    FillPixel(topHalf, brush, x, y);
                }
            }

            image.Mutate(x => x.DrawImage(topHalf, new Point(0, 0), 1f));
            topHalf.Mutate(x => x.Flip(FlipMode.Vertical));
            image.Mutate(x => x.DrawImage(topHalf, new Point(0, 4), 1f));

            return image;
        }
    }
}
