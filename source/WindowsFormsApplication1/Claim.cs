// <copyright file="Claim.cs" company="None">Hello, world!</copyright>
// <author>Shamara Thompson</author>
// <date>08/08/2016</date>

namespace PremiumQuoteGenerator
{
    using System;

    /// <summary>
    /// Represents a claim made by a Driver in the past.
    /// </summary>
    public class Claim
    {
        /// <summary>
        /// The date the claim was made.
        /// </summary>
        private DateTime myDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="Claim"/> class.
        /// </summary>
        /// <param name="date">The date the claim was made</param>
        public Claim(DateTime date)
        {
            this.myDate = date;
        }

        /// <summary>
        /// Gets or sets the date the claim was made.
        /// </summary>
        public DateTime Date
        {
            get { return this.myDate; }
            set { this.myDate = value; }
        }
    }
}
