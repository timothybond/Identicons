using System.Security.Cryptography;

namespace Identicons
{
    /// <summary>
    /// Utility class for hashing strings and extracting useful values out of a hash.
    /// </summary>
    public static class Hash
    {
        const int Length = 64;

        public static MD5 MD5 = MD5.Create();

        public static ulong GetHash(string str)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(str);
            var hashBytes = MD5.ComputeHash(bytes);

            return BitConverter.ToUInt64(hashBytes);
        }

        /// <summary>
        /// Gets a number that represents the given number of sequential bits from the hash, at the given start index.
        /// </summary>
        /// <param name="hash">The hash from which to extract.</param>
        /// <param name="number">The number of bits to extract.</param>
        /// <param name="startIndex">The index of the initial bit to extract.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int GetBits(ulong hash, int number, int startIndex)
        {
            if (number > 5)
            {
                throw new ArgumentException("Cannot get more than 5 bits (for safety reasons).");
            }

            if (startIndex + number > Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            ulong result = hash;

            result = result << startIndex;
            result = result >> (Length - number);

            // Gross cast, but we limit it to 5 bits above so it shouldn't matter
            return (int)result;
        }

        /// <summary>
        /// Gets the given hash value, with everything moved one position to the left and the initial bit at the end.
        /// </summary>
        public static ulong GetWraparoundOffset(ulong hash)
        {
            var lastBit = (ulong)GetBits(hash, 1, 0);
            return hash << 1 | lastBit;
        }

        /// <summary>
        /// Reverses the bits of the given hash value.
        /// </summary>
        public static ulong Reverse(ulong hash)
        {
            for (var i = 0; i < Length / 2; i++)
            {
                var higher = (ulong)GetBits(hash, 1, i);
                var lower = (ulong)GetBits(hash, 1, Length - 1 - i);

                if (higher == 1)
                {
                    hash |= (higher << i);
                }
                else
                {
                    hash &= (ulong.MaxValue - (1u << i));
                }

                if (lower == 1)
                {
                    hash |= (lower << (Length - 1 - i));
                }
                else
                {
                    hash &= (ulong.MaxValue - (1ul << (Length - 1 - i)));
                }
            }

            return hash;
        }
    }
}
