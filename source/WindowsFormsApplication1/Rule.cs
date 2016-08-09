// <copyright file="Rule.cs" company="None">Hello, world!</copyright>
// <author>Shamara Thompson</author>
// <date>09/08/2016</date>

namespace PremiumQuoteGenerator
{
    /// <summary>
    /// Defines a representation of a rule for calculating policies as defined in the database.
    /// </summary>
    public class Rule
    {
        /// <summary>
        /// The area of input that this rule pertains to
        /// </summary>
        private string _field;

        /// <summary>
        /// The first value of the conditional range of this rule, if applicable.
        /// </summary>
        private string _value1;

        /// <summary>
        /// The second value of the conditional range of this rule, if applicable.
        /// </summary>
        private string _value2;

        /// <summary>
        /// The condition that the input must satisfy with the conditional range given if the
        /// rule is to apply, if applicable.
        /// </summary>
        private string _condition;

        /// <summary>
        /// The operation that should be performed on the quote if the rule is applied
        /// </summary>
        private string _operation;

        /// <summary>
        /// A value attached to the operation to be performed on the quote if the rule is applied.
        /// </summary>
        private string _operationValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rule"/> class.
        /// </summary>
        /// <param name="f">The field</param>
        /// <param name="v1">First conditional value</param>
        /// <param name="v2">Second  conditional value</param>
        /// <param name="c">The condition</param>
        /// <param name="o">The operation</param>
        /// <param name="ov">The operation value</param>
        public Rule(string f, string v1, string v2, string c, string o, string ov)
        {
            Field = f.Trim();
            Value1 = v1.Trim();
            Value2 = v2.Trim();
            Condition = c.Trim();
            Operation = o.Trim();
            OperationValue = ov.Trim();
        }

        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        public string Field
        {
            get
            {
                return _field;
            }

           set
            {
                _field = value;
            }
        }

        /// <summary>
        /// Gets or sets value1.
        /// </summary>
        public string Value1
        {
            get
            {
                return _value1;
            }

            set
            {
                _value1 = value;
            }
        }

        /// <summary>
        /// Gets or sets value2.
        /// </summary>
        public string Value2
        {
            get
            {
                return _value2;
            }

            set
            {
                _value2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        public string Condition
        {
            get
            {
                return _condition;
            }

            set
            {
                _condition = value;
            }
        }

        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        public string Operation
        {
            get
            {
                return _operation;
            }

            set
            {
                _operation = value;
            }
        }

        /// <summary>
        /// Gets or sets the operation value
        /// </summary>
        public string OperationValue
        {
            get
            {
                return _operationValue;
            }

            set
            {
                _operationValue = value;
            }
        }
    }
}
