namespace Identicons
{
    /// <summary>
    /// A generator for an Identicon.
    /// </summary>
    public interface IIdenticonGenerator
    {
        /// <summary>
        /// Generates an identicon and writes it (in PNG format) to the output stream.
        /// </summary>
        public Task Generate(ulong hash, IColorScheme colorScheme, Stream outputStream);
    }
}
