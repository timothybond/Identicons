using Identicons.ColorSchemes;
using Identicons.Generators;

namespace Identicons
{
    public static class RootGenerator
    {
        /// <summary>
        /// Generates an Identicon for the given input string,
        /// and writes it out to the given stream.
        /// 
        /// This is the main entry point to the Identicon logic
        /// for consumers.
        /// </summary>
        public static async Task Generate(string str, Stream outputStream)
        {
            var hash = Hash.GetHash(str);

            // Note: originally three bits were reserved for selecting the generator.
            // It would be something of a pointless headache to adjust everything else,
            // so we'll just say that the third bit is "reserved" in case we want to
            // add more generators later.
            var generatorIndex = Hash.GetBits(hash, 2, 0);
            var colorSchemeIndex = Hash.GetBits(hash, 2, 3);

            var hue = Hash.GetBits(hash, 3, 5) * 45f;

            IIdenticonGenerator generator = generatorIndex switch
            {
                0 => new HorizontalMirrorGenerator(),
                1 => new VerticalMirrorGenerator(),
                2 => new DoubleMirrorGenerator(),
                3 => new RotatedSideGenerator(),
                _ => throw new NotImplementedException()
            };

            IColorScheme colorScheme = colorSchemeIndex switch
            {
                0 => new MonochromeColorScheme(hue),
                1 => new DivergentColorScheme(hue),
                2 => new TriadicColorScheme(hue),
                3 => new TetradicColorScheme(hue),
                _ => throw new NotImplementedException()
            };

            await generator.Generate(hash, colorScheme, outputStream);
        }
    }
}
