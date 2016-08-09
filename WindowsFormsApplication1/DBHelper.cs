// <copyright file="DBHelper.cs" company="None">Hello, world!</copyright>
// <author>Shamara Thompson</author>
// <date>08/08/2016</date>

namespace PremiumQuoteGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    /// <summary>
    /// Object with methods for fetching data from the database.
    /// </summary>
    public class DBHelper
    {
        /// <summary>
        /// The SQL connection.
        /// </summary>
        private SqlConnection sqlConnection;

        /// <summary>
        /// The SQL command for putting in our queries.
        /// </summary>
        private SqlCommand cmd;

        /// <summary>
        /// The SQL data reader for parsing the results of our queries.
        /// </summary>
        private SqlDataReader reader;

        /// <summary>
        /// The string for creating a conn
        /// </summary>
        private const string connectionString = "Data Source=AGAPE\\SQLEXPRESS;Initial Catalog=InsuranceRulesDB;Integrated Security=True";

        /// <summary>
        /// Initializes a new instance of the <see cref="DBHelper"/> class.
        /// </summary>
        public DBHelper()
        {
            this.sqlConnection = new SqlConnection(connectionString);
            this.cmd = new SqlCommand();
        }

        /// <summary>
        /// Fetches the list of occupations from the database as a List of strings.
        /// </summary>
        /// <returns>The List<string> of accepted occupations.</string></returns>
        public List<string> GetOccupations()
        {
            List<string> occupations = new List<string>();
            this.cmd.CommandText = "SELECT * FROM occupation ORDER BY name ASC";
            this.cmd.CommandType = System.Data.CommandType.Text;
            this.cmd.Connection = this.sqlConnection;
            this.sqlConnection.Open();
            this.reader = this.cmd.ExecuteReader();
            while (this.reader.Read())
            {
                occupations.Add(this.reader.GetValue(0).ToString().Trim());
            }

            this.sqlConnection.Close();
            return occupations;
        }

        /// <summary>
        /// Fetches the list of rules from the database as a List of Rule objects.
        /// </summary>
        /// <returns>The List of rules from the database.</returns>
        public List<Rule> GetRules()
        {
            List<Rule> rules = new List<Rule>();
            this.cmd.CommandText = "SELECT * FROM insuranceRule";
            this.cmd.CommandType = System.Data.CommandType.Text;
            this.cmd.Connection = this.sqlConnection;
            this.sqlConnection.Open();
            this.reader = this.cmd.ExecuteReader();
            while (this.reader.Read())
            {
                object[] values = new object[7];
                this.reader.GetValues(values);
                    rules.Add(new Rule(
                        values[1].ToString(),
                        values[2].ToString(),
                        values[3].ToString(),
                        values[4].ToString(),
                        values[5].ToString(),
                        values[6].ToString()));
            }

            this.sqlConnection.Close();
            return rules;
        }
    }
}
