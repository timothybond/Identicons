using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Identicons.Generators
{
    /// <summary>
    /// A generator that fills in the top half of an image,
    /// then rotates it by 180 degrees to get the bottom half.
    /// </summary>
    public class RotatedSideGenerator : BaseIdenticonGenerator
    {
        protected override Image GenerateImage(ulong hash, IColorScheme colorScheme)
        {
            // Note that this gets returned, so it shouldn't be disposed here,
            // but will be disposed in BaseIdenticonGenerator.Generate(...).
            Image image = new Image<Rgba32>(8, 8);
            using Image topHalf = new Image<Rgba32>(4, 8);

            var brushes = GetBrushes(colorScheme);

            var rhash = Hash.Reverse(hash);

            for (var x = 0; x < 4; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    var pixelIndex = y * 4 + x;
                    Brush brush = brushes[GetBrushIndex(hash, pixelIndex)];

                    FillPixel(topHalf, brush, x, y);
                }
            }

            image.Mutate(x => x.DrawImage(topHalf, new Point(0, 0), 1f));
            topHalf.Mutate(x => x.Rotate(RotateMode.Rotate180));
            image.Mutate(x => x.DrawImage(topHalf, new Point(4, 0), 1f));

            return image;
        }
    }
}
