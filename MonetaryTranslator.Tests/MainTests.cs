using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonetaryTranslator.App;
using System;

namespace MonetaryTranslator
{
    [TestClass]
    public class MainTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MustBeValid()
        {
            var result = (Program.ValidateInput("aasdasdwe£$%^"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MustBePositive()
        {
            var result = (Program.ValidateInput("-£100"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MustBeWithinRange()
        {
            var a = Program.ValidateInput("-£1");
            var b = Program.ValidateInput("-£1,000,000,000");
        }
        
        [TestMethod]
        public void MustExtractWholeAndRemainder()
        {
            var remainder = "";
            var input = "123,456,789.99";
            var whole = Program.GetWholeAndRemainder(input, ref remainder);
            Assert.IsTrue(whole == "123,456,789");
            Assert.IsTrue(remainder == "99");
        }

        [TestMethod]
        public void MustBeAbleToTranslateAnyNumberFromAGroup()
        {
            var a = Program.TranslateGroup("001");
            Assert.IsTrue(a == "one");
            var b = Program.TranslateGroup("011");
            Assert.IsTrue(b == "eleven");
            var c = Program.TranslateGroup("123");
            Assert.IsTrue(c == "one hundred and twentythree");

        }

        [TestMethod]
        public void MustTranslateAndAddDenomination()
        {
            Assert.IsTrue(Program.TranslateAndAddDenomination("100", "pounds") == "one hundred pounds");
        }

        [TestMethod]
        public void MustTranslateAGroupOfNumbers()
        {
            Assert.IsTrue(Program.TranslateGroup("123") == "one hundred and twentythree");
        }

        [TestMethod]
        public void MustWorkWithPence()
        {
            Assert.IsTrue(Program.ProcessInput("£3,496,002.08") == "three million four hundred and ninetysix thousand two pounds and eight pence");
        }

        [TestMethod]
        public void MustWorkWithoutPence()
        {
            Assert.IsTrue(Program.ProcessInput("£3,496,002") == "three million four hundred and ninetysix thousand two pounds");
        }
    }
}
