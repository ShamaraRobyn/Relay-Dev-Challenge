// <copyright file="ValidatorTest.cs" company="None">Hello, world!</copyright>
// <author>Shamara Thompson</author>
// <date>09/08/2016</date>

namespace PremiumQuoteGenerator
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Class for testing the Validator class.
    /// </summary>
    [TestClass]
    public class ValidatorTest
    {
        /// <summary>
        /// The validator object to be used for testing
        /// </summary>
        private Validator validator;

        /// <summary>
        /// DateTime representation of the current date
        /// </summary>
        private DateTime today = DateTime.Today;

        /// <summary>
        /// DateTime representation of yesterday
        /// </summary>
        private DateTime yesterday = DateTime.Today.AddDays(-1);

        /// <summary>
        /// DateTime representation of tomorrow
        /// </summary>
        private DateTime tomorrow = DateTime.Today.AddDays(1);

        /// <summary>
        /// The minimum allowed age for a driver
        /// </summary>
        private int min_age = PremiumQuoteGenerator.Validator.MIN_AGE;

        /// <summary>
        /// Tests the Validator constructor.
        /// </summary>
        [TestMethod]
        public void Validator()
        {
            validator = new Validator();
            Assert.IsTrue(validator.ValidOccupations.Contains("Accountant"), "Accountant was not added as a valid occupation");
            Assert.IsTrue(validator.ValidOccupations.Contains("Chauffeur"), "Chauffeur was not added as a valid occupation");
        }

        /// <summary>
        /// Tests the IsValidStartDate method of Validator.
        /// </summary>
        [TestMethod]
        public void IsValidStartDate()
        {
            validator = new Validator();
            Assert.IsTrue(validator.IsValidStartDate(tomorrow), "Tomorrow should be a valid start date.");
            Assert.IsTrue(validator.IsValidStartDate(today), "Today should be a valid start date.");
            Assert.IsFalse(validator.IsValidStartDate(yesterday), "Yesterday should not be a valid start date.");
        }

        /// <summary>
        /// Tests the IsValidName method of Validator.
        /// </summary>
        [TestMethod]
        public void IsValidName()
        {
            validator = new Validator();
            Assert.IsTrue(validator.IsValidName("Brian"), "Brian should be a valid name");
        }

        /// <summary>
        /// Tests the IsValidOccupation method of Validator
        /// </summary>
        [TestMethod]
        public void IsValidOccupation()
        {
            validator = new Validator();
            Assert.IsTrue(validator.IsValidOccupation("Chauffeur"), "Chauffeur should be a valid occupation");
            Assert.IsTrue(validator.IsValidOccupation("Accountant"), "Accountant should be a valid occupation");
            Assert.IsFalse(validator.IsValidOccupation("Worrier"), "Worrier should be a valid occupation");
        }

        /// <summary>
        /// Tests the IsValidDOB method of Validator
        /// </summary>
        [TestMethod]
        public void IsValidDOB()
        {
            validator = new Validator();
            Assert.AreEqual(validator.IsValidDOB(today.AddYears(-min_age)), 1, "21 years ago should be a valid date of birth");
        }

        /// <summary>
        /// Tests the IsValidClaimDate method of Validator
        /// </summary>
        [TestMethod]
        public void IsValidClaimDate()
        {
            validator = new PremiumQuoteGenerator.Validator();
            Assert.IsTrue(validator.IsValidClaimDate(today), "Today should be a valid claim date.");
            Assert.IsTrue(validator.IsValidClaimDate(yesterday), "Yesterday should be a valid claim date.");
            Assert.IsFalse(validator.IsValidClaimDate(tomorrow), "Tomorrow should not be a valid claim date.");
        }
    }
}
