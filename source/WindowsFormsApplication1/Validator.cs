// <copyright file="Validator.cs" company="None">Hello, world!</copyright>
// <author>Shamara Thompson</author>
// <date>09/08/2016</date>

namespace PremiumQuoteGenerator
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class containing methods for user input validation.
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// The minimum allowed age.
        /// </summary>
        public const int MIN_AGE = 21;

        /// <summary>
        /// The maximum allowed age.
        /// </summary>
        public const int MAX_AGE = 75;

        /// <summary>
        /// The list of valid occupations.
        /// </summary>
        private List<string> _validOccupations;

        /// <summary>
        /// DateTime representation of the current date.
        /// </summary>
        private DateTime today;

        /// <summary>
        /// Object for retrieving valid occupations from the database.
        /// </summary>
        private DBHelper dbhelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="Validator"/> class.
        /// </summary>
        public Validator()
        {
            //dbhelper = new DBHelper();
            //_validOccupations = dbhelper.GetOccupations();
            _validOccupations = new List<string>(new string[]{"Accountant", "Chauffeur", "Other"});
            today = DateTime.Today;
        }

        /// <summary>
        /// Gets the list of valid occupations.
        /// </summary>
        public List<string> ValidOccupations
        {
            get
            {
                return _validOccupations;
            }
        }

        /// <summary>
        /// Determines whether a given DateTime is valid as a start date for a new policy.
        /// </summary>
        /// <param name="date">The start date of the policy.</param>
        /// <returns>True if date is today or later. False if date is before the current date.</returns>
        public bool IsValidStartDate(DateTime date)
        {
            if (date.Date < today)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether a given string is valid as a driver name.
        /// </summary>
        /// <param name="name">The name of the driver.</param>
        /// <returns>True if the string is valid as a name.</returns>
        public bool IsValidName(string name)
        {
            return true;
        }

        /// <summary>
        /// Determines whether a given string is a valid occupation.
        /// </summary>
        /// <param name="occupation">The driver's occupation</param>
        /// <returns>Returns true if the given string occupation is contained in the set of valid occupations.</returns>
        public bool IsValidOccupation(string occupation)
        {
            if (this.ValidOccupations.Contains(occupation))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether a given DateTime is valid as a driver's date of birth.
        /// </summary>
        /// <param name="dob">The driver's date of birth</param>
        /// <returns>Returns true if someone with this date of birth would be as old or older than the
        /// minimum allowed age in years, and as young or younger than the maximum allowed age.</returns>
        public int IsValidDOB(DateTime dob)
        {
            int age = today.Year - dob.Year;
            if (dob > today.AddYears(-age))
            {
                age--;
            }
            /*
            if (age < MIN_AGE)
            {
                return -1;
            }

            if (age > MAX_AGE)
            {
                return 0;
            } */

            return 1;
        }

        /// <summary>
        /// Determines whether a given DateTime is valid as a previous insurance claim date.
        /// </summary>
        /// <param name="claimDate">The date that the claim was made.</param>
        /// <returns>Returns true if the date is not in the future.</returns>
        public bool IsValidClaimDate(DateTime claimDate)
        {
            if (claimDate.Date > today)
            {
                return false;
            }

            return true;
        }
    }
}
