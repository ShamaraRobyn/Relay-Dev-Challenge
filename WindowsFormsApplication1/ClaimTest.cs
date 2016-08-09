// <copyright file="ClaimTest.cs" company="None">Hello, world!</copyright>
// <author>Shamara Thompson</author>
// <date>08/08/2016</date>

namespace PremiumQuoteGenerator
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Testing class for the Claim class.
    /// </summary>
    [TestClass]
    public class ClaimTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimTest"/> class.
        /// </summary>
        [TestMethod]
        public void Claim()
        {
            DateTime t = new DateTime();
            Claim c = new Claim(t);
            Assert.AreEqual(c.Date, t, "Claim was not created with the correct date.");
        }
    }
}
