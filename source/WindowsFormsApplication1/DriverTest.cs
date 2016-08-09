// <copyright file="DriverTest.cs" company="None">Hello, world!</copyright>
// <author>Shamara Thompson</author>
// <date>08/08/2016</date>

namespace PremiumQuoteGenerator
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for the Driver class.
    /// </summary>
    [TestClass]
    public class DriverTest
    {
        /// <summary>
        /// Name to be given to a test driver
        /// </summary>
        private string name = "Test";

        /// <summary>
        /// Occupation to be given to a test driver
        /// </summary>
        private string occupation = "Occupation";

        /// <summary>
        /// Date of Birth to be given to  a test driver
        /// </summary>
        private DateTime dateOfBirth = DateTime.Today.AddYears(-21);

        /// <summary>
        /// Test date 1
        /// </summary>
        private DateTime d1 = new DateTime();

        /// <summary>
        /// Test date 2
        /// </summary>
        private DateTime d2 = new DateTime();

        /// <summary>
        /// Test date 3
        /// </summary>
        private DateTime d3 = new DateTime();

        /// <summary>
        /// Test date 4
        /// </summary>
        private DateTime d4 = new DateTime();

        /// <summary>
        /// Test date 5
        /// </summary>
        private DateTime d5 = new DateTime();

        /// <summary>
        /// Initializes a new instance of the <see cref="DriverTest"/> class.
        /// </summary>
        [TestMethod]
        public void Driver()
        {
            Driver d = new Driver(this.name, this.occupation, this.dateOfBirth);
            Assert.AreEqual(d.Name, this.name, "Driver was not created with the correct name.");
            Assert.AreEqual(d.Occupation, this.occupation, "Driver was not created with the correct occupation");
        }

        /// <summary>
        /// Tests method addClaim() of Driver.
        /// </summary>
        [TestMethod]
        public void AddClaim()
        {
            Driver d = new Driver(this.name, this.occupation, this.dateOfBirth);
            d.AddClaim(d1);
            Assert.AreEqual(d1, d.Claims[0].Date, "Claim was not added successfully");
        }

        /// <summary>
        /// Tests the correct functionality of a Driver when adding claims and checks
        /// that the correct number of drivers can be added.
        /// </summary>
        [TestMethod]
        public void AddFiveClaims()
        {
            Driver d = new Driver(this.name, this.occupation, this.dateOfBirth);
            d.AddClaim(d1);
            d.AddClaim(d2);
            d.AddClaim(d3);
            d.AddClaim(d4);
            d.AddClaim(d5);
            Assert.AreEqual(d1, d.Claims[0].Date, "Claim 1 does not match correct date");
            Assert.AreEqual(d2, d.Claims[1].Date, "Claim 2 does not match correct date");
            Assert.AreEqual(d3, d.Claims[2].Date, "Claim 3 does not match correct date");
            Assert.AreEqual(d4, d.Claims[3].Date, "Claim 4 does not match correct date");
            Assert.AreEqual(d5, d.Claims[4].Date, "Claim 5 does not match correct date");
            d.AddClaim(d1);
            Assert.AreEqual(d.Claims.Count, 5, "It should not be possible to add a sixth claim");
        }

        /// <summary>
        /// Tests method removeClaim of Driver.
        /// </summary>
        [TestMethod]
        public void RemoveClaim()
        {
            Driver d = new Driver(this.name, this.occupation, this.dateOfBirth);
            d.AddClaim(d1);
            Assert.AreEqual(d.Claims.Count, 1, "Claim was not added successfully");
            d.RemoveClaim(0);
            Assert.AreEqual(d.Claims.Count, 0, "Claim was not removed successfully");
        }
    }
}
