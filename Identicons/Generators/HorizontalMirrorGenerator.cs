using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Identicons.Generators
{
    /// <summary>
    /// A generator that fills in the left half of an image,
    /// then mirrors it horizontally across the midline.
    /// </summary>
    public class HorizontalMirrorGenerator : BaseIdenticonGenerator
    {
        protected override Image GenerateImage(ulong hash, IColorScheme colorScheme)
        {
            // Note that this gets returned, so it shouldn't be disposed here,
            // but will be disposed in BaseIdenticonGenerator.Generate(...).
            Image image = new Image<Rgba32>(8, 8);
            using Image leftHalf = new Image<Rgba32>(4, 8);

            var brushes = GetBrushes(colorScheme);

            var rhash = Hash.Reverse(hash);

            for (var x = 0; x < 4; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    var pixelIndex = y * 4 + x; // Max 31
                    Brush brush = brushes[GetBrushIndex(hash, pixelIndex)];

                    FillPixel(leftHalf, brush, x, y);
                }
            }

            image.Mutate(x => x.DrawImage(leftHalf, new Point(0, 0), 1f));
            leftHalf.Mutate(x => x.Flip(FlipMode.Horizontal));
            image.Mutate(x => x.DrawImage(leftHalf, new Point(4, 0), 1f));

            return image;
        }
    }
}
