using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Identicons.Generators
{
    /// <summary>
    /// A generator that fills in a single corner, then flips it across all midlines
    /// to get a picture that is mirrored both vertically and horizonally.
    /// </summary>
    public class DoubleMirrorGenerator : BaseIdenticonGenerator
    {
        protected override Image GenerateImage(ulong hash, IColorScheme colorScheme)
        {
            // Note that this gets returned, so it shouldn't be disposed here,
            // but will be disposed in BaseIdenticonGenerator.Generate(...).
            Image image = new Image<Rgba32>(8, 8);

            using Image corner = new Image<Rgba32>(4, 4);

            var brushes = GetBrushes(colorScheme);

            var rhash = Hash.Reverse(hash);

            for (var x = 0; x < 4; x++)
            {
                for (var y = 0; y < 4; y++)
                {
                    var pixelIndex = y * 4 + x; // Max 15
                    Brush brush = brushes[GetBrushIndex(hash, pixelIndex)];

                    FillPixel(corner, brush, x, y);
                }
            }

            image.Mutate(x => x.DrawImage(corner, new Point(0, 0), 1f));
            corner.Mutate(x => x.Flip(FlipMode.Horizontal));
            image.Mutate(x => x.DrawImage(corner, new Point(4, 0), 1f));
            corner.Mutate(x => x.Flip(FlipMode.Vertical));
            image.Mutate(x => x.DrawImage(corner, new Point(4, 4), 1f));
            corner.Mutate(x => x.Flip(FlipMode.Horizontal));
            image.Mutate(x => x.DrawImage(corner, new Point(0, 4), 1f));

            return image;
        }
    }
}
