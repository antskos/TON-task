using NUnit.Framework;
using System.Collections.Generic;

namespace SubstringFinder.Tests
{
    [TestFixture]
    public class ExtensionMethodsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(")", ExpectedResult = false)]
        [TestCase("]]]]]]", ExpectedResult = false)]
        [TestCase("}]}]", ExpectedResult = false)]
        [TestCase("{", ExpectedResult = false)]
        [TestCase("((((", ExpectedResult = false)]
        [TestCase("{}]", ExpectedResult = false)]
        [TestCase("[([]]", ExpectedResult = false)]
        [TestCase("][(){}]", ExpectedResult = false)]
        [TestCase("[}]{", ExpectedResult = false)]
        public bool IsBalancedParenthesisMethod_UnbalancedInputString_ExpectedFalse(string str)
        {
            return ExtensionMethods.ExtensionMethods.IsBalancedParenthesis(str);
        }

        [TestCase("{}", ExpectedResult = true)]
        [TestCase("]{}[", ExpectedResult = true)]
        [TestCase("(){}[]", ExpectedResult = true)]
        [TestCase("{([(){}[]])}", ExpectedResult = true)]
        [TestCase(")}][][{()([])(", ExpectedResult = true)]
        public bool IsBalancedParenthesisMethod_BalancedInputString_ExpectedTrue(string str)
        {
            return ExtensionMethods.ExtensionMethods.IsBalancedParenthesis(str);
        }
     
        // "FindSubs" method can find only non looped substings
        [Test]
        public void FindSubsMethod_UnbalancedInputString_ExpectedListOf_CBS_Substrings1()
        {
            #region Arrange

            string str = "]a(abc)";
            List<(int, int)> expectedList = new List<(int, int)> {(1, 1), (2, 6), (3, 5) };
            expectedList.Sort();

            #endregion

            #region Act

            List<(int,int)> result =  ExtensionMethods.ExtensionMethods.FindSubs(str);
            result.Sort();

            #endregion

            #region Assert

            Assert.That(result, Is.Unique);
            Assert.That(result, Is.EquivalentTo(expectedList));

            #endregion
        }

        [Test]
        public void FindSubsMethod_UnbalancedInputString_ExpectedListOf_CBS_Substrings2()
        {
            #region Arrange

            string str = "()adf{abd}}cccc]";
            List<(int, int)> expectedList = new List<(int, int)> { (0, 1), (2, 4), (6, 8), (5, 9), (11,14) };
            expectedList.Sort();

            #endregion

            #region Act

            List<(int, int)> result = ExtensionMethods.ExtensionMethods.FindSubs(str);
            result.Sort();

            #endregion

            #region Assert

            Assert.That(result, Is.Unique);
            Assert.That(result, Is.EquivalentTo(expectedList));

            #endregion
        }

        [Test]
        public void FindSubsMethod_UnbalancedInputString_ExpectedListOf_CBS_Substrings3()
        {
            #region Arrange

            string str = "]bc(cd)ab[";
            List<(int, int)> expectedList = new List<(int, int)> { (1, 2), (4, 5), (3, 6), (7, 8) };
            expectedList.Sort();

            #endregion

            #region Act

            List<(int, int)> result = ExtensionMethods.ExtensionMethods.FindSubs(str);
            result.Sort();

            #endregion

            #region Assert

            Assert.That(result, Is.Unique);
            Assert.That(result, Is.EquivalentTo(expectedList));

            #endregion
        }

        [Test]
        public void FindSubsMethod_BalancedInputString_ExpectedListOf_CBS_Substrings1()
        {
            #region Arrange

            string str = "(())";
            List<(int, int)> expectedList = new List<(int, int)> { (1, 2), (0, 3) };
            expectedList.Sort();

            #endregion

            #region Act

            List<(int, int)> result = ExtensionMethods.ExtensionMethods.FindSubs(str);
            result.Sort();

            #endregion

            #region Assert

            Assert.That(result, Is.Unique);
            Assert.That(result, Is.EquivalentTo(expectedList));

            #endregion
        }

        [Test]
        public void FindSubsMethod_UnbalancedInputString_ExpectedEmptyListOfSubstrings1()
        {
            #region Arrange

            string str = "]}})}]";
            
            #endregion

            #region Act

            List<(int, int)> result = ExtensionMethods.ExtensionMethods.FindSubs(str);
            
            #endregion

            #region Assert

            Assert.That(result, Is.Empty);
            
            #endregion
        }

        [Test]
        public void FindSubsMethod_UnbalancedInputString_ExpectedEmptyListOfSubstrings2()
        {
            #region Arrange

            string str = "({[({[";

            #endregion

            #region Act

            List<(int, int)> result = ExtensionMethods.ExtensionMethods.FindSubs(str);

            #endregion

            #region Assert

            Assert.That(result, Is.Empty);

            #endregion
        }

        [Test]
        public void FindSubsMethod_UnbalancedInputString_ExpectedEmptyListOfSubstrings3()
        {
            #region Arrange

            string str = "])}{([";

            #endregion

            #region Act

            List<(int, int)> result = ExtensionMethods.ExtensionMethods.FindSubs(str);

            #endregion

            #region Assert

            Assert.That(result, Is.Empty);

            #endregion
        }

        [Test]
        public void FindNoCBSSubs_UnbalancedInputStringAndListOfCBSPairs_ExpectedNoCBSListOfSubs1()
        {
            #region Arrange

            string str = "](())[";
            List<(int, int)> inputList = new List<(int, int)> { (1, 4) };
            List<(int, int)> expectedList = new List<(int, int)> { (0, 0), (5, 5) };
            expectedList.Sort();

            #endregion

            #region Act

            List<(int, int)> result = ExtensionMethods.ExtensionMethods.FindNoCBSSubs(str, inputList);
            result.Sort();

            #endregion

            #region Assert

            Assert.That(result, Is.Unique);
            Assert.That(result, Is.EquivalentTo(expectedList));

            #endregion
        }

        [Test]
        public void FindNoCBSSubs_UnbalancedInputStringsAndListOfCBSPairs_ExpectedNoCBSListOfSubs2()
        {
            #region Arrange

            string str = "](())[(a)[{";
            List<(int, int)> inputList = new List<(int, int)> { (1, 4), (6, 8) };
            List<(int, int)> expectedList = new List<(int, int)> { (0, 0), (5, 5), (9, 10) };
            expectedList.Sort();

            #endregion

            #region Act

            List<(int, int)> result = ExtensionMethods.ExtensionMethods.FindNoCBSSubs(str, inputList);
            result.Sort();

            #endregion

            #region Assert

            Assert.That(result, Is.Unique);
            Assert.That(result, Is.EquivalentTo(expectedList));

            #endregion
        }

        [Test]
        public void FindNoCBSSubs_UnbalancedInputStringsAndListOfCBSPairs_ExpectedNoCBSListOfSubs3()
        {
            #region Arrange

            string str = "(())}[(a)[{";
            List<(int, int)> inputList = new List<(int, int)> { (0, 3), (6, 8) };
            List<(int, int)> expectedList = new List<(int, int)> { (4, 5), (9, 10) };
            expectedList.Sort();

            #endregion

            #region Act

            List<(int, int)> result = ExtensionMethods.ExtensionMethods.FindNoCBSSubs(str, inputList);
            result.Sort();

            #endregion

            #region Assert

            Assert.That(result, Is.Unique);
            Assert.That(result, Is.EquivalentTo(expectedList));

            #endregion
        }


        [Test]
        public void FindNoCBSSubs_UnbalancedInputStringsAndListOfCBSPairs_ExpectedNoCBSListOfSubs4()
        {
            #region Arrange

            string str = "abc(cde";
            List<(int, int)> inputList = new List<(int, int)> { (0, 2), (4, 6) };
            List<(int, int)> expectedList = new List<(int, int)> { (3, 3) };
            expectedList.Sort();

            #endregion

            #region Act

            List<(int, int)> result = ExtensionMethods.ExtensionMethods.FindNoCBSSubs(str, inputList);
            result.Sort();

            #endregion

            #region Assert

            Assert.That(result, Is.Unique);
            Assert.That(result, Is.EquivalentTo(expectedList));

            #endregion
        }

        [Test]
        public void FindNoCBSSubs_UnbalancedInputStringsAndListOfCBSPairs_ExpectedNoCBSListOfSubs5()
        {
            #region Arrange

            string str = "ad[]}))ad]{}){()dfav";
            List<(int, int)> inputList = new List<(int, int)> { (0, 3), (7, 8), (10, 11), (14, 19) };
            List<(int, int)> expectedList = new List<(int, int)> { (4, 6), (9, 9), (12, 13) };
            expectedList.Sort();

            #endregion

            #region Act

            List<(int, int)> result = ExtensionMethods.ExtensionMethods.FindNoCBSSubs(str, inputList);
            result.Sort();

            #endregion

            #region Assert

            Assert.That(result, Is.Unique);
            Assert.That(result, Is.EquivalentTo(expectedList));

            #endregion
        }

        //tests for looped subs
        [Test]
        public void FindLoopedSubMethod_UnbalancedInputStringAndListOfNonCBSSubs_ReturnsMaximalLoopedSub1()
        {
            #region Arrange

            string str = "))ad]{}((";
            List<(int, int)> inputList = new List<(int, int)> { (0, 1), (4, 4), (7,8) };
            string expected = "{}(())ad";

            #endregion

            #region Act

            string result = ExtensionMethods.ExtensionMethods.FindLoopedSub(str, inputList);

            #endregion

            #region Assert

            Assert.That(result, Is.EqualTo(expected));

            #endregion
        }

        [Test]
        public void FindLoopedSubMethod_UnbalancedInputStringAndListOfNonCBSSubs_ReturnsMaximalLoopedSub2()
        {
            #region Arrange

            string str = ")}][{(";
            List<(int, int)> inputList = new List<(int, int)> { (0, 5) };
            string expected = "[{()}]";

            #endregion

            #region Act

            string result = ExtensionMethods.ExtensionMethods.FindLoopedSub(str, inputList);

            #endregion

            #region Assert

            Assert.That(result, Is.EqualTo(expected));

            #endregion
        }

        [Test]
        public void FindLoopedSubMethod_UnbalancedInputStringAndListOfNonCBSSubs_ReturnsMaximalLoopedSub3()
        {
            #region Arrange

            string str = "ad[]}))ad]{}){()dfav";
            List<(int, int)> inputList = new List<(int, int)> { (4, 6), (9, 9), (12, 13) };
            string expected = "{()dfavad[]}";

            #endregion

            #region Act

            string result = ExtensionMethods.ExtensionMethods.FindLoopedSub(str, inputList);

            #endregion

            #region Assert

            Assert.That(result, Is.EqualTo(expected));

            #endregion
        }

        [Test]
        public void FindLoopedSubMethod_UnbalancedInputStringAndListOfNonCBSSubs_ReturnsMaximalLoopedSub4()
        {
            #region Arrange

            string str = "abc)";
            List<(int, int)> inputList = new List<(int, int)> { (3, 3) };
            string expected = "abc";

            #endregion

            #region Act

            string result = ExtensionMethods.ExtensionMethods.FindLoopedSub(str, inputList);

            #endregion

            #region Assert

            Assert.That(result, Is.EqualTo(expected));

            #endregion
        }

        [Test]
        public void FindLoopedSubMethod_UnbalancedInputStringAndListOfNonCBSSubs_ReturnsMaximalLoopedSub5()
        {
            #region Arrange

            string str = "){][})((";
            List<(int, int)> inputList = new List<(int, int)> { (0, 7) };
            string expected = "()";

            #endregion

            #region Act

            string result = ExtensionMethods.ExtensionMethods.FindLoopedSub(str, inputList);

            #endregion

            #region Assert

            Assert.That(result, Is.EqualTo(expected));

            #endregion
        }

        [Test]
        public void FindLoopedSubMethod_UnbalancedInputStringAndListOfNonCBSSubs_ReturnsMaximalLoopedSub6()
        {

            #region Arrange

            string str = ")asc][[]({}){}](";
            List<(int, int)> inputList = new List<(int, int)> { (0, 0), (4, 5), (15,15) };
            string expected = "[[]({}){}]()asc";

            #endregion

            #region Act

            string result = ExtensionMethods.ExtensionMethods.FindLoopedSub(str, inputList);

            #endregion

            #region Assert

            Assert.That(result, Is.EqualTo(expected));

            #endregion
        }

        [Test]
        public void FindLoopedSubMethod_UnbalancedInputStringAndListOfNonCBSSubs_ReturnsMaximalLoopedSub7()
        {

            #region Arrange

            string str = "]sh(dh)";
            List<(int, int)> inputList = new List<(int, int)> { (0, 0) };
            string expected = "sh(dh)";

            #endregion

            #region Act

            string result = ExtensionMethods.ExtensionMethods.FindLoopedSub(str, inputList);

            #endregion

            #region Assert

            Assert.That(result, Is.EqualTo(expected));

            #endregion
        }
    }
}