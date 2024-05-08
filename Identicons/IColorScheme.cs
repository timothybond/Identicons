using SixLabors.ImageSharp;

namespace Identicons
{
    /// <summary>
    /// A set of colors for use when painting an Identicon.
    /// 
    /// By convention, each color scheme returns up to 8 colors,
    /// although since it's not enforced that index is in the range 0-7,
    /// implementers should make sure it works for any input value.
    /// </summary>
    public interface IColorScheme
    {
        Color GetColor(int index);
    }
}
