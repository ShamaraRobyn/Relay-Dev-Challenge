// <copyright file="QuoteHandler.cs" company="None">Hello, world!</copyright>
// <author>Shamara Thompson</author>
// <date>08/08/2016</date>

namespace PremiumQuoteGenerator
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An instance of this class is used for storing entered policy details and rules,
    /// and for generating the premium. 
    /// </summary>
    public class QuoteHandler
    {
        /// <summary>
        /// The initial value of a premium before adjustment
        /// </summary>
        private const int INITIAL_PREMIUM = 50000; // £500.00

        /// <summary>
        /// The maximum number of drivers allowed on a policy
        /// </summary>
        private const int MAX_DRIVERS = 5;

        /// <summary>
        /// The minimum number of drivers required to set up a policy
        /// </summary>
        private const int MIN_DRIVERS = 5;

        /// <summary>
        /// The current calculated premium
        /// </summary>
        private int _premium;

        /// <summary>
        /// The premium start date
        /// </summary>
        private DateTime _startDate;

        /// <summary>
        /// The list of drivers input by the user
        /// </summary>
        private List<Driver> _drivers;

        /// <summary>
        /// DBHelper for fetching data from the database 
        /// </summary>
        private DBHelper dbhelper;

        /// <summary>
        /// Validator for ensuring validity of user input from fields.
        /// </summary>
        private Validator validator;

        /// <summary>
        /// The list of rules to be applied when calculating a quote.
        /// </summary>
        private List<Rule> rules;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteHandler"/> class.
        /// </summary>
        public QuoteHandler()
        {
            _drivers = new List<Driver>();
            //dbhelper = new DBHelper();
            //rules = dbhelper.GetRules();
            validator = new Validator();
        }

        /// <summary>
        /// Gets the current estimated insurance premium.
        /// As it is used to calculate an amount in money, the value is stored as an integer to preserve accuracy.
        /// </summary>
        public int Premium
        {
            get { return _premium; }
        }

        /// <summary>
        /// Gets or sets the start date of the policy.
        /// </summary>
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        /// <summary>
        /// Gets the list of drivers that have been added to the policy.
        /// </summary>
        public List<Driver> Drivers
        {
            get { return _drivers; }
        }

        /// <summary>
        /// Adds a driver with the given strings name and occupation to the policy.
        /// </summary>
        /// <param name="name">The new driver's name</param>
        /// <param name="occupation">The new driver's occupation</param>
        /// <param name="dateOfBirth">The new driver's date of birth</param>
        /// <returns>Returns true if it was possible to add another driver to the policy.</returns>
        public bool AddDriver(string name, string occupation, DateTime dateOfBirth)
        {
            if (Drivers.Count >= MAX_DRIVERS)
            {
                return false;
            }

            Driver newDriver = new Driver(name, occupation, dateOfBirth);
            Drivers.Add(newDriver);
            return true;
        }

        /// <summary>
        /// Returns a string containing either a monetary value (the calculated quote)
        /// or a declination message if this is not possible.
        /// </summary>
        /// <returns>Calculated quote or declination message in string.</returns>
        public string GetPremiumString()
        {
            if (!validator.IsValidStartDate(StartDate))
            {
                Console.WriteLine(StartDate);
                return "Start date of policy";
            }

            if (Drivers.Count == 0)
            {
                return "£--.--";
            }

            //count total claims
            var nClaims = 0;

            foreach (Driver d in Drivers)
            {
                //count claims
                nClaims += d.Claims.Count;

                if(d.Claims.Count > 2)
                {
                    return d.Name + " has more \n than 2 claims.";
                }

                if(nClaims > 3)
                {
                    return "Policy has more than \n 3 claims.";
                }

                // Decline if any fields have been neglected.
                if (d.Name.Equals("New Driver"))
                {
                    return "Driver name required";
                }

                if (d.Occupation.Equals("--Select One--"))
                {
                    return "Occupation required.";
                }

                if (d.DateOfBirth.Equals(DateTime.Today))
                {
                    return "Date of birth required.";
                }
            }

            //set to initial premium
            _premium = INITIAL_PREMIUM;

            //Occupation rules
            foreach(Driver d in Drivers)
            {
                if (d.Occupation.Equals("Chauffeur"))
                {
                    _premium = (int) (_premium * 1.1);
                    break;
                }
            }
            foreach (Driver d in Drivers)
            {
                if (d.Occupation.Equals("Accountant"))
                {
                    _premium = (int)(_premium * 0.9);
                    break;
                }
            }

            //Age rules

            var youngest = Drivers[0].GetAge();
            var oldest = Drivers[0].GetAge();
            foreach (Driver d in Drivers)
            {
                if (d.GetAge() < youngest)
                {
                    youngest = d.GetAge();
                }

                if (d.GetAge() > oldest)
                {
                    oldest = d.GetAge();
                }
            }

            if (youngest < 21)
            {
                return "Age of youngest driver";
            }

            if (youngest >= 21 && youngest <= 25)
            {
                _premium = (int)(_premium * 1.2);
            }

            if  (youngest > 25 && youngest <= 75)
            {
                _premium = (int)(_premium * 0.9);
            }

            if (oldest > 75)
            {
                return "Age of oldest driver";
            }

            //Claim rules
            foreach (Driver d in Drivers){
                foreach (Claim c in d.Claims){
                    if (c.Date <= DateTime.Today.AddDays(1).AddMonths(-0)
                                            && c.Date >= DateTime.Today.AddMonths(-12))
                    {
                        _premium = (int)(_premium * 1.2);
                    }
                    if (c.Date <= DateTime.Today.AddDays(1).AddMonths(-13)
                        && c.Date >= DateTime.Today.AddMonths(-60))
                    {
                        _premium = (int)(_premium * 1.1);
                    }
                }
            }

                /*

                foreach (Rule r in rules)
                {
                    if (r.Operation.Equals("set"))
                    {
                        if (r.Field.Equals("null"))
                        {
                            _premium = Convert.ToInt32(r.OperationValue);
                        }
                    }
                    else if (r.Operation.Equals("incr") || r.Operation.Equals("decr") || r.Operation.Equals("decline"))
                    {
                        if (r.Field.Equals("occupation"))
                        {
                            foreach (Driver d in Drivers)
                            {
                                if (d.Occupation.Equals(r.Value1))
                                {
                                    if (r.Operation.Equals("incr"))
                                    {
                                        _premium += (int)(_premium * Convert.ToDouble(r.OperationValue));
                                    }

                                    if (r.Operation.Equals("decr"))
                                    {
                                        _premium -= (int)(_premium * Convert.ToDouble(r.OperationValue));
                                    }

                                    break;
                                }
                            }
                        }
                        else if (r.Field.Equals("age"))
                        {
                            var youngest = Drivers[0].GetAge();
                            var oldest = Drivers[0].GetAge();
                            foreach (Driver d in Drivers){
                                if (d.GetAge() < youngest)
                                {
                                    youngest = d.GetAge();
                                }

                                if (d.GetAge() > oldest)
                                {
                                    oldest = d.GetAge();
                                }

                            }
                            if (r.Condition.Equals("bt"))
                            {
                                if ( youngest >= Convert.ToInt32(r.Value1)
                                    && youngest <= Convert.ToInt32(r.Value2))
                                {
                                    if (r.Operation.Equals("incr"))
                                    {
                                        _premium += (int)(_premium * Convert.ToDouble(r.OperationValue));
                                    }

                                    if (r.Operation.Equals("decr"))
                                    {
                                        _premium -= (int)(_premium * Convert.ToDouble(r.OperationValue));
                                    }
                                }
                            }

                            if (r.Condition.Equals("lt"))
                            {
                                if (youngest < Convert.ToInt32(r.Value1))
                                {
                                    if (r.Operation.Equals("decline"))
                                    {
                                        return "Age of youngest driver";
                                    }
                                }
                            }

                            if (r.Condition.Equals("gt"))
                            {
                                if(oldest > Convert.ToInt32(r.Value1))
                                {
                                    if (r.Operation.Equals("decline"))
                                    {
                                        return "Age of oldest driver";
                                    }
                                }
                            }
                        }
                        else if (r.Field.Equals("claimdate"))
                        {
                            foreach (Driver d in Drivers)
                            {
                                foreach (Claim c in d.Claims)
                                {
                                    if (r.Condition.Equals("bt"))
                                    {
                                        if (c.Date <= DateTime.Today.AddDays(1).AddMonths(-Convert.ToInt32(r.Value1))
                                            && c.Date >= DateTime.Today.AddMonths(-Convert.ToInt32(r.Value2)))
                                        {
                                            if (r.Operation.Equals("incr"))
                                            {
                                                _premium += (int)(_premium * Convert.ToDouble(r.OperationValue));
                                            }

                                            if (r.Operation.Equals("decr"))
                                            {
                                                _premium -= (int)(_premium * Convert.ToDouble(r.OperationValue));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }*/

                return "£" + ((Premium - (Premium % 100)) / 100) + "." + (Premium % 100).ToString("00");
            
    }

            public bool removeDriver(int index)
            {
                if (index < 0 || Drivers.Count <= index)
                {
                    return false;
                }
                Drivers.RemoveAt(index);
                return true;
            } 
            
    }
}
