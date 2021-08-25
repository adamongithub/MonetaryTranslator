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
        public void MustBeAbleToTranslateFromArray()
        {
            var a = Program.TranslateFromArray('0', new[] { "a","b","c" }, "prefix ", " suffix");
            Assert.IsTrue(a == "prefix a suffix");
        }

        [TestMethod]
        public void MustTranslateTens()
        {
            Assert.IsTrue(Program.ProcessTens("01").Contains("one"));
            Assert.IsTrue(Program.ProcessTens("11", 0, 1).Contains("eleven"));
        }
        [TestMethod]
        public void MustWorkWithPence()
        {

        }

        [TestMethod]
        public void MustBeWithinoutPence()
        {

        }
    }
}
