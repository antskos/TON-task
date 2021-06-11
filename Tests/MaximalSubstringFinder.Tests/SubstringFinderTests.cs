using NUnit.Framework;
using System.Collections.Generic;


namespace SubstringFinder.Tests
{
    [TestFixture]
    public class SubstringFinderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("*@a1b2c3", ExpectedResult = "abc")]
        [TestCase("аrбtвy", ExpectedResult = "rty")]
        [TestCase("**A({)BC]]&&76W", ExpectedResult = "A({)BC]]W")]
        [TestCase("!1@2#3$4%5^6&7*8-", ExpectedResult = "")]
        public string FilterStringMethod_AnyInputStrings_ExpectedEnglishLettersWith3TypeOfBracketsStrings(string str)
        {
            return SubstringFinder.FilterString(str);
        }

        [Test]
        public void FindMaxSubstringMethod_BalancedInputStrings_ExpectedInfinite(
            [Values("abc", "ABC", "()", "(abc)", "]abc[", "]{a(b[c]d)f}()[")] string str)
        {

            #region Arrange

            double subLength;
            string expected = "Infinite";

            #endregion

            #region Act

            string result = SubstringFinder.FindMaxSubstring(str, out subLength);

            #endregion

            #region Assert

            Assert.Multiple(() =>
            {
                Assert.AreEqual(double.PositiveInfinity, subLength);
                Assert.That(result, Is.EqualTo(expected));
            });

            #endregion
        }

        [Test]
        public void MaximazeSubstringsMethod_CollectionOfSubstringPairs_SubstringsWithMaxGluingIntervals() 
        {
            #region Arrange

            List<(int, int)> inputCollection = new List<(int, int)> { (1, 3), (2, 5) };
            IEnumerable<(int, int)> expectedCollection = new List<(int, int)> { (1, 5) };

            #endregion

            #region Act

            IEnumerable<(int, int)> result = SubstringFinder.MaximazeSubstrings(inputCollection);
            
            #endregion

            #region Assert

            Assert.That(result, Is.EquivalentTo(expectedCollection));

            #endregion
        }

        [Test]
        public void MaximazeSubstringsMethod_CollectionOfSubstringPairs_SubstringsWithMaxGluingIntervals1()
        {
            #region Arrange

            List<(int, int)> inputCollection = new List<(int, int)> { (0, 3), (2, 4), (5, 6), (8, 15), (10, 11), (13, 13), (12, 14) };
            IEnumerable<(int, int)> expectedCollection = new List<(int, int)> { (0, 6), (8, 15) };

            #endregion

            #region Act

            IEnumerable<(int, int)> result = SubstringFinder.MaximazeSubstrings(inputCollection);

            #endregion

            #region Assert

            Assert.That(result, Is.EquivalentTo(expectedCollection));

            #endregion
        }

        [Test]
        public void MaximazeSubstringsMethod_CollectionOfSubstringPairs_SubstringsWithMaxGluingIntervals2()
        {
            #region Arrange

            List<(int, int)> inputCollection = new List<(int, int)> {};
            IEnumerable<(int, int)> expectedCollection = new List<(int, int)> {};

            #endregion

            #region Act

            IEnumerable<(int, int)> result = SubstringFinder.MaximazeSubstrings(inputCollection);

            #endregion

            #region Assert

            Assert.That(result, Is.EquivalentTo(expectedCollection));

            #endregion
        }

        [Test]
        public void FindMaxSubstringMethod_UnbalancedInputString_ExpectedMaximalSubstring1()
        {
            #region Arrange
            string str = "]({hdb}b)}]asd(sda))(d{sad{[";
            double subLength;

            string expected = "sad{[]({hdb}b)}";

            #endregion

            #region Act

            string result = SubstringFinder.FindMaxSubstring(str, out subLength);

            #endregion

            #region Assert

            Assert.Multiple(() =>
            {
                Assert.AreEqual(15, subLength);
                Assert.That(result, Is.EqualTo(expected));
            });

            #endregion
        }

        [Test]
        public void FindMaxSubstringMethod_UnbalancedInputString_ExpectedMaximalSubstring2()
        {
            #region Arrange
            string str = "{sh(dh)";
            double subLength;

            string expected = "sh(dh)";

            #endregion

            #region Act

            string result = SubstringFinder.FindMaxSubstring(str, out subLength);

            #endregion

            #region Assert

            Assert.Multiple(() =>
            {
                Assert.AreEqual(6, subLength);
                Assert.That(result, Is.EqualTo(expected));
            });

            #endregion
        }
    }
}
