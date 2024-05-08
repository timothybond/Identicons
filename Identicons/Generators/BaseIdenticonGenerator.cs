using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace Identicons.Generators
{
    /// <summary>
    /// Base class with shared logic for the different generators.
    /// </summary>
    public abstract class BaseIdenticonGenerator : IIdenticonGenerator
    {
        /// <inheritdoc/>
        public async Task Generate(ulong hash, IColorScheme colorScheme, Stream outputStream)
        {
            using var image = GenerateImage(hash, colorScheme);
            await image.SaveAsPngAsync(outputStream);
        }

        /// <summary>
        /// Generates an <see cref="Image"/> containing an Identicon based on the given hash and color scheme.
        /// </summary>
        protected abstract Image GenerateImage(ulong hash, IColorScheme colorScheme);

        /// <summary>
        /// Gets the index of the brush (out of an assumed 8 brushes) to be used for
        /// the pixel with the given index.
        /// 
        /// Note that different patterns have different ways to describe their pixel
        /// indexes, such that we can't map straight from x/y coordinates here.
        /// 
        /// We use 3-bit chunks to determine the brush index, and we have a 128-bit hash
        /// to work with, although the first 8 bits (sort of) are used to select the
        /// generator (which determines layout), color scheme, and main color.
        /// 
        /// For maximum flexibility, we use a rolling 3-bit window, so we can advance it
        /// up to the (128-3)th bit, and then just for safety's sake we also use the reversed
        /// bits, for a total of (128 - 8 - 3) + (128 - 3) possible values (i.e., 242).
        /// 
        /// Note that this is more than sufficient for much larger identicons than we currently make.
        /// </summary>
        protected int GetBrushIndex(ulong hash, int pixelIndex)
        {
            if (pixelIndex < 0 || pixelIndex > 242)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (pixelIndex < 128 - 8 - 3)
            {
                return Hash.GetBits(hash, 3, 8 + pixelIndex);
            }
            else
            {
                var rhash = Hash.Reverse(hash);
                return Hash.GetBits(rhash, 3, pixelIndex - (128 - 8 - 3));
            }
        }

        /// <summary>
        /// Gets the 8 brushes associated with a given color scheme.
        /// </summary>
        protected IReadOnlyList<Brush> GetBrushes(IColorScheme colorScheme)
        {
            var brushes = new List<Brush>();
            for (var i = 0; i < 8; i++)
            {
                brushes.Add(new SolidBrush(colorScheme.GetColor(i)));
            }

            return brushes;
        }

        /// <summary>
        /// Paints a single pixel in an image.
        /// </summary>
        /// <param name="image">Target image.</param>
        /// <param name="brush">Brush to paint.</param>
        /// <param name="x">X-coordinate of the pixel.</param>
        /// <param name="y">Y-coordinate of the pixel.</param>
        protected void FillPixel(Image image, Brush brush, float x, float y)
        {
            image.Mutate(im => im.FillPolygon(brush, new PointF(x, y), new PointF(x + 1, y), new PointF(x + 1, y + 1), new PointF(x, y + 1)));
        }
    }
}
