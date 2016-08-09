// <copyright file="Driver.cs" company="None">Hello, world!</copyright>
// <author>Shamara Thompson</author>
// <date>08/08/2016</date>

namespace PremiumQuoteGenerator
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a driver to be attached as a customer to the policy.
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// the maximum number of claims a driver can have attached to them.
        /// </summary>
        private const int MAXCLAIMS = 5;

        /// <summary>
        /// The name of the Driver
        /// </summary>
        private string myName;

        /// <summary>
        /// The date of birth of the driver
        /// </summary>
        private DateTime myDateOfBirth;

        /// <summary>
        /// The driver's occupation
        /// </summary>
        private string myOccupation;

        /// <summary>
        /// The claims associated with the driver
        /// </summary>
        private List<Claim> myClaims;

        /// <summary>
        /// Initializes a new instance of the <see cref="Driver"/> class.
        /// </summary>
        /// <param name="name">The name of the driver</param>
        /// <param name="occupation">The driver's occupation</param>
        /// <param name="dob">The driver's date of birth</param>
        public Driver(string name, string occupation, DateTime dob)
        {
            this.Name = name;
            this.Occupation = occupation;
            this.DateOfBirth = dob;
            this.myClaims = new List<Claim>();
        }

        /// <summary>
        /// Gets or sets the name of a driver
        /// </summary>
        public string Name
        {
            get { return this.myName; }
            set { this.myName = value; }
        }

        /// <summary>
        /// Gets or sets the date of birth of a driver
        /// </summary>
        public DateTime DateOfBirth
        {
            get { return this.myDateOfBirth;  }
            set { this.myDateOfBirth = value; }
        }

        /// <summary>
        /// Gets or sets the occupation of a driver
        /// </summary>
        public string Occupation
        {
            get { return this.myOccupation; }
            set { this.myOccupation = value; }
        }

        /// <summary>
        /// Gets a driver's list of Claims
        /// </summary>
        public List<Claim> Claims
        {
            get { return this.myClaims; }
        }

        /// <summary>
        /// Adds a claim to this Driver's list of claims that was made on the given DateTime.
        /// </summary>
        /// <param name="date">The date that the claim was made.</param>
        /// <returns>True if the claim was successfully added, false if there were too many claims to add another.</returns>
        public bool AddClaim(DateTime date)
        {
            if (this.Claims.Count >= MAXCLAIMS)
            {
                return false;
            }

            Claim newClaim = new Claim(date);
            this.Claims.Add(newClaim);
            return true;
        }

        /// <summary>
        /// Removes the claim at the given index from the Driver's list of claims.
        /// </summary>
        /// <param name="index">The list index of the driver.</param>
        /// <returns>True if the operation was possible</returns>
        public bool RemoveClaim(int index)
        {
            if (this.Claims.Count < index + 1 || index < 0)
            {
                return false;
            }

            this.Claims.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Returns the age in years of the driver.
        /// </summary>
        /// <returns>The age in years of the driver.</returns>
        public int GetAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - this.DateOfBirth.Year;
            if (this.DateOfBirth > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
