// <copyright file="QuoteHandlerTest.cs" company="None">Hello, world!</copyright>
// <author>Shamara Thompson</author>
// <date>09/08/2016</date>

namespace PremiumQuoteGenerator
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Class for testing the QuoteHandler class.
    /// </summary>
    [TestClass]
    public class QuoteHandlerTest
    {
        /// <summary>
        /// Test for the AddDriver method of QuoteHandler.
        /// </summary>
        [TestMethod]
        public void AddDriver()
        {
            QuoteHandler qh = new QuoteHandler();
            qh.AddDriver("Brian", "Accountant", DateTime.Today.AddYears(-22));
            Assert.AreEqual(qh.Drivers.Count, 1, "There should be one driver"); 
            Assert.AreEqual("Brian", qh.Drivers[0].Name);
            Assert.AreEqual("Accountant", qh.Drivers[0].Occupation);
            Assert.AreEqual(DateTime.Today.AddYears(-22), qh.Drivers[0].DateOfBirth);
        }

        /// <summary>
        /// Test for the RemoveDriver method of QuoteHandler.
        /// </summary>
        [TestMethod]
        public void RemoveDriver()
        {
            QuoteHandler qh = new QuoteHandler();
            qh.AddDriver("Brian", "Accountant", DateTime.Today.AddYears(-22));
            qh.removeDriver(0);
            Assert.AreEqual(qh.Drivers.Count, 0, "There should be no drivers");
        }

        /// <summary>
        /// Test for the GetPremiumString method of QuoteHandler.
        /// </summary>
        [TestMethod]
        public void GetPremiumString()
        {
            QuoteHandler qh = new QuoteHandler();

            qh.StartDate = DateTime.Today.AddDays(-1);
            var pstring = qh.GetPremiumString();
            Assert.AreEqual("Start date of policy", pstring, "Should be declined for start date");

            qh.StartDate = DateTime.Today;
            qh.AddDriver("New Driver", "--Select One--", DateTime.Today);
            pstring = qh.GetPremiumString();
            Assert.AreEqual("Driver name required", pstring, "Should be declined for driver name");

            qh.Drivers[0].Name = "Brian";
            pstring = qh.GetPremiumString();
            Assert.AreEqual("Occupation required.", pstring, "Should be declined for occupation");

            qh.Drivers[0].Occupation = "Other";
            pstring = qh.GetPremiumString();
            Assert.AreEqual("Date of birth required.", pstring, "Should be declined for dob");

            qh.Drivers[0].DateOfBirth = DateTime.Today.AddDays(-1);
            pstring = qh.GetPremiumString();
            Assert.AreEqual("Age of youngest driver", pstring, "Should be declined for young age");

            qh.Drivers[0].DateOfBirth = DateTime.Today.AddYears(-76);
            pstring = qh.GetPremiumString();
            Assert.AreEqual("Age of oldest driver", pstring, "Should be declined for old age");

            qh.Drivers[0].DateOfBirth = DateTime.Today.AddYears(-26);
            pstring = qh.GetPremiumString();
            Assert.AreEqual("£450.00", pstring, "Should be 10% below base");

            qh.Drivers[0].DateOfBirth = DateTime.Today.AddYears(-25);
            pstring = qh.GetPremiumString();
            Assert.AreEqual("£600.00", pstring, "Should be 20% above base");

            qh.Drivers[0].Occupation = "Accountant";
            pstring = qh.GetPremiumString();
            Assert.AreEqual("£540.00", pstring, "Should have taken off 10%");

            qh.Drivers[0].Occupation = "Chauffeur";
            pstring = qh.GetPremiumString();
            Assert.AreEqual("£660.00", pstring, "Should have added 10% instead");

            qh.Drivers[0].AddClaim(DateTime.Today);
            pstring = qh.GetPremiumString();
            Assert.AreEqual("£792.00", pstring, "Should have added 20%");

            qh.Drivers[0].RemoveClaim(0);
            qh.Drivers[0].AddClaim(DateTime.Today.AddYears(-2));
            pstring = qh.GetPremiumString();
            Assert.AreEqual("£726.00", pstring, "Should have added 10% instead");

            qh.Drivers[0].AddClaim(DateTime.Today.AddYears(-2));
            pstring = qh.GetPremiumString();
            Assert.AreEqual("£798.60", pstring, "Should have added another 10%");

            qh.AddDriver("Sue", "Accountant", DateTime.Today.AddYears(-27));
            pstring = qh.GetPremiumString();
            Assert.AreEqual("£718.74", pstring, "Should be base + 20% - 10% + 10% + 10% + 10%");

            qh.Drivers[1].AddClaim(DateTime.Today);
            qh.Drivers[1].AddClaim(DateTime.Today);
            qh.Drivers[1].AddClaim(DateTime.Today);
            pstring = qh.GetPremiumString();
            Assert.AreEqual("Sue has more \n than 2 claims.", pstring, "should be denied for # of claims");

            qh.Drivers[1].RemoveClaim(0);
            pstring = qh.GetPremiumString();
            Assert.AreEqual("Policy has more than \n 3 claims.", pstring, "should be denied for # of claims");
        }
    }
}
