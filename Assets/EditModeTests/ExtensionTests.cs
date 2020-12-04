using NUnit.Framework;

namespace EditModeTests
{
    public class ExtensionTests
    {
        [TestCase(999, 9, ExpectedResult = "000000999")]
        [TestCase(99999, 3, ExpectedResult = "99999")]
        [TestCase(-9, 4, ExpectedResult = "0009")]
        [TestCase(99, 0, ExpectedResult = "99")]
        [TestCase(99, -1, ExpectedResult = "99")]
        [TestCase(0, 9, ExpectedResult = "000000000")]
        [TestCase(0, 0, ExpectedResult = "0")]
        [TestCase(0, -1, ExpectedResult = "0")]
        [TestCase(-1, -1, ExpectedResult = "1")]
        public string ScoreConvertTest(int number, int count)
        {
            return number.ToString().ConvertScore(count);
        }
    }
}