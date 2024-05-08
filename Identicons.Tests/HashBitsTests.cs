namespace Identicons.Tests
{
    public class HashBitsTests
    {
        // Note: the binary literal syntax is (somewhat) easier to follow,
        // but it automatically treats things as ulong or long depending on if they're negative.
        // 
        // Also, some of the (ulong) casts below are unnecessary, but included for readability.

        [Test]
        [TestCase((ulong)0b_0111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111, 2, 0, 1)]
        [TestCase((ulong)0b_0111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111, 2, 2, 3)]
        [TestCase((ulong)0b0000_0000_0000_0000_0000_0000_0000_1111_0000_0000_0000_0000_0000_0000_0000_0000, 1, 31, 1)]
        [TestCase((ulong)0b0000_0000_0000_0000_0000_0000_0000_1010_0000_0000_0000_0000_0000_0000_0000_0000, 4, 28, 10)]
        [TestCase((ulong)0b0000_0000_0000_0000_0000_0000_0000_1010_0000_0000_0000_0000_0000_0000_0000_0000, 3, 28, 5)]
        public void GetBits(ulong hash, int bits, int startIndex, int expected)
        {
            var result = Hash.GetBits(hash, bits, startIndex);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(
            (ulong)0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0011_0111,
            (ulong)0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0110_1110)]
        [TestCase(
            (ulong)0b1100_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000,
            (ulong)0b1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001)]
        [TestCase(
            (ulong)0b1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111,
            (ulong)0b1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111)]
        [TestCase(
            (ulong)0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000,
            (ulong)0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000)]
        public void GetWraparoundOffset(ulong value, ulong expected)
        {
            var result = Hash.GetWraparoundOffset(value);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(
            (ulong)0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000,
            (ulong)0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000)]
        [TestCase(
            (ulong)0b1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111,
            (ulong)0b1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111)]
        [TestCase(
            (ulong)0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0101_0011,
            (ulong)0b1100_1010_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000)]
        [TestCase(
            (ulong)0b1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000,
            (ulong)0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001)]
        [TestCase(
            (ulong)0b1000_1100_1010_0110_0001_1001_0101_1101_0011_1011_0111_1111_0000_1000_0100_1100,
            (ulong)0b0011_0010_0001_0000_1111_1110_1101_1100_1011_1010_1001_1000_0110_0101_0011_0001)]

        public void Reverse(ulong value, ulong expected)
        {
            var result = Hash.Reverse(value);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}