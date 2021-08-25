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
        public void MustWorkWithPence()
        {

        }

        [TestMethod]
        public void MustBeWithinoutPence()
        {

        }
    }
}
