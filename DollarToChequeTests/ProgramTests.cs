using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DollarToCheque;

namespace DollarToChequeTests
{
    [TestClass]
    public class ProgramTests
    {
        Program program = new Program();
        List<string> expectedList = new List<string>();

        //*         INPUT VALIDATION        *//
        [TestMethod]
        public void ValidateWholeNumberValidatesTrue()
        {
            Assert.IsTrue(program.validate("12345"));            
        }

        [TestMethod]
        public void ValidateTwoDecimalValidatesTrue()
        {
            Assert.IsTrue(program.validate("123.45"));
        }

        [TestMethod]
        public void ValidateOneDecimalValidatesFalse()
        {
            Assert.IsFalse(program.validate("1234.5"));
        }

        [TestMethod]
        public void ValidateMoreThanTwoDecimalValidatesFalse()
        {
            Assert.IsFalse(program.validate("12.345"));
        }

        [TestMethod]
        public void ValidateNonNumberValidatesFalse()
        {
            Assert.IsFalse(program.validate("abcd"));
        }

        [TestMethod]
        public void ValidateNumberAndNonNumberValidatesFalse()
        {
            Assert.IsFalse(program.validate("1234abcd"));
        }

        [TestMethod]
        public void ValidateNegativeNumberValidatesFalse()
        {
            Assert.IsFalse(program.validate("-123"));
        }
        [TestMethod]
        public void ValidateZeroValidatesFalse()
        {
            Assert.IsFalse(program.validate("0"));
        }
        [TestMethod]
        public void ValidateQuintillionValidatesFalse()
        {
            Assert.IsFalse(program.validate("1000000000000000000"));
        }

        //*         ConvertDollarToText        *//
        [TestMethod]
        public void ConvertDollarToTextCents()
        {
            Assert.AreEqual(program.convertDollarToText("0.12"), "ZERO DOLLARS AND TWELVE CENTS");
        }
        [TestMethod]
        public void ConvertDollarToTextSmall()
        {
            Assert.AreEqual(program.convertDollarToText("12.34"), "TWELVE DOLLARS AND THIRTY-FOUR CENTS");
        }

        [TestMethod]
        public void ConvertDollarToTextThousands()
        {
            Assert.AreEqual(program.convertDollarToText("1234.56"), "ONE THOUSAND TWO HUNDRED AND THIRTY-FOUR DOLLARS AND FIFTY-SIX CENTS");
        }

        [TestMethod]
        public void ConvertDollarToTextLargeNumber()
        {
            Assert.AreEqual(program.convertDollarToText("1234567.89"), "ONE MILLION TWO HUNDRED AND THIRTY-FOUR THOUSAND FIVE HUNDRED AND SIXTY-SEVEN DOLLARS AND EIGHTY-NINE CENTS");
        }
        //*         OneToText        *//
        [TestMethod]
        public void OneToTextNumberReturnsTextNumber()
        {
            Assert.AreEqual(program.OneToText("1"), "ONE");
        }

        [TestMethod]
        public void OneToTextZeroReturnsBlank()
        {
            Assert.AreEqual(program.OneToText("0"), "");
        }

        //*         TwoToText        *//
        [TestMethod]
        public void TwoToTextZeroReturnsBlank()
        {
            Assert.AreEqual(program.TwoToText("00"), "");
        }

        [TestMethod]
        public void TwoToTextSingleReturnsTextSingle()
        {
            Assert.AreEqual(program.TwoToText("01"), "ONE");
        }

        [TestMethod]
        public void TwoToTextTeenReturnsTextTeen()
        {
            Assert.AreEqual(program.TwoToText("15"), "FIFTEEN");
        }

        [TestMethod]
        public void TwoToTextMultipleOfTenReturnsTextMultipleOfTen()
        {
            Assert.AreEqual(program.TwoToText("50"), "FIFTY");
        }

        [TestMethod]
        public void TwoToTextTwoDigitNumReturnsTextualTwoDigitNum()
        {
            Assert.AreEqual(program.TwoToText("55"), "FIFTY-FIVE");
        }

        //*         ThreeToText        *//
        [TestMethod]
        public void ThreeToTextZeroReturnsBlank()
        {
            Assert.AreEqual(program.TwoToText("000"), "");
        }

        [TestMethod]
        public void ThreeToTextSingleReturnsSingle()
        {
            Assert.AreEqual(program.TwoToText("009"), "NINE");
        }

        [TestMethod]
        public void ThreeToTextDoubleReturnsDouble()
        {
            Assert.AreEqual(program.ThreeToText("099", false), "NINETY-NINE");
        }

        [TestMethod]
        public void ThreeToTextHundredReturnsHundred()
        {
            Assert.AreEqual(program.ThreeToText("100", false), "ONE HUNDRED");
        }

        [TestMethod]
        public void ThreeToTextMixedReturnsTextMixed()
        {
            Assert.AreEqual(program.ThreeToText("123", false), "ONE HUNDRED AND TWENTY-THREE");
        }

        //*         splitStringIntoBlocks        *//
        [TestMethod]
        public void SplitStringStrLen1ReturnsLen1()
        {
            expectedList.Add("1");
            CollectionAssert.AreEqual(program.splitStringIntoBlocks("1", 3), expectedList);
        }
        [TestMethod]
        public void SplitStringStrLen2ReturnsLen2()
        {
            expectedList.Add("11");
            CollectionAssert.AreEqual(program.splitStringIntoBlocks("11", 3), expectedList);
        }
        [TestMethod]
        public void SplitStringStrLen3ReturnsLen3()
        {
            expectedList.Add("111");
            CollectionAssert.AreEqual(program.splitStringIntoBlocks("111", 3), expectedList);
        }
        public void SplitStringStrLen6ReturnsTwoStrings()
        {
            expectedList.Add("111");
            expectedList.Add("111");
            CollectionAssert.AreEqual(program.splitStringIntoBlocks("111111", 3), expectedList);
        }
    }
}
