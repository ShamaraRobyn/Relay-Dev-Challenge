// <copyright file="Form.cs" company="None">Hello, world!</copyright>
// <author>Shamara Thompson</author>
// <date>08/08/2016</date>

namespace PremiumQuoteGenerator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// Form for displaying the UI.
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        /// QuoteHandler responsible for storing input data and calculating output.
        /// </summary>
        private QuoteHandler quoteHandler;

        /// <summary>
        /// Validator for ensuring validity of user input from fields.
        /// </summary>
        private Validator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class. (For the UI form)
        /// </summary>
        public Main()
        {
            InitializeComponent();

            quoteHandler = new QuoteHandler();
            validator = new Validator();
            PopulateDriverList();
            occupationComboBox.DataSource = validator.ValidOccupations;
            startDatePicker.Value = DateTime.Today;
        }

        /// <summary>
        /// Validates and updates the selected driver's name when the field value is updated.
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">The event arguments</param>
        private void DriverName_Updated(object sender, EventArgs e)
        {
            if (quoteHandler.Drivers.Count == 0)
            {
                return;
            }

            var newName = driverNameField.Text;
            if (validator.IsValidName(newName))
            {
                quoteHandler.Drivers[driversListBox.SelectedIndex].Name = newName;
                UpdateDriverList();
            }
        }

        /// <summary>
        /// Validates and updates the selected driver's occupation when the field value is updated.
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">The event arguments</param>
        private void DriverOccupation_Updated(object sender, EventArgs e)
        {
            if (quoteHandler.Drivers.Count == 0)
            {
                return;
            }

            var newOccupation = occupationComboBox.Text;
            if (validator.IsValidOccupation(newOccupation))
            {
                quoteHandler.Drivers[driversListBox.SelectedIndex].Occupation = newOccupation;
                UpdateDriverList();
            }
        }

        /// <summary>
        /// Validates and updates the selected driver's date of birth when the field value is updated.
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">The event arguments</param>
        private void DriverDateOfBirth_Updated(object sender, EventArgs e)
        {
            if (quoteHandler.Drivers.Count == 0)
            {
                return;
            }

            quoteHandler.Drivers[driversListBox.SelectedIndex].DateOfBirth = dateOfBirthPicker.Value;
            UpdateDriverList();
            
        }

        /// <summary>
        /// Updates the driver detail fields and claim list when the driver list selection is changed.
        /// </summary>
        /// <param name="sender">The source of this event</param>
        /// <param name="e">The event arguments</param>
        private void DriversList_Selected_Index_Changed(object sender, EventArgs e)
        {
            if (quoteHandler.Drivers.Count == 0)
            {
                return;
            }

            if (driversListBox.SelectedIndex < 0)
            {
                return;
            }

            driverDetailsGroupBox.Enabled = true;
            PopulateDriverDetails();
            PopulateClaimList();

        }

        /// <summary>
        /// Invoked when the add driver button is clicked, and passes the field data into a new
        /// Driver object.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event arguments</param>
        private void AddDriverButton_Click(object sender, EventArgs e)
        {
            if (quoteHandler.AddDriver("New Driver", "--Select One--", DateTime.Today))
            {
                PopulateDriverList();
                PopulateClaimList();
                Console.WriteLine("Hi");
            }
            else
            {
                MessageBox.Show("Can't add driver", "Error");
            }
        }

        /// <summary>
        /// Invoked when the add claim button is clicked, passes the value from the claim
        /// date picker to create a new Claim associated with the selected driver.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event arguments</param>
        private void AddClaimButton_Click(object sender, EventArgs e)
        {
            DateTime newClaimDate = newClaimDatePicker.Value;
            int driverIndex = driversListBox.SelectedIndex;
            if (validator.IsValidClaimDate(newClaimDate))
            {
                // If a new claim is successfully added
                if (quoteHandler.Drivers[driverIndex].AddClaim(newClaimDate))
                {
                    PopulateClaimList();
                }
                else
                {
                    MessageBox.Show("Can't add claim.");
                }
            }
            else
            {
                MessageBox.Show("Invalid claim date - cannot be in future");
            }
        }

        /// <summary>
        /// Populates the claims list box with the claims associated with the current driver.
        /// Populates it with the text "No Claims" if there are none.
        /// </summary>
        private void PopulateClaimList()
        {
            int driverIndex = driversListBox.SelectedIndex;

            // If the driver index has not been set, or there are no claims
            if (driverIndex == -1 || quoteHandler.Drivers[driverIndex].Claims.Count == 0)
            {
                List<string> emptylist = new List<string>();
                emptylist.Add("No claims");
                claimsListBox.DataSource = emptylist;
                claimsListBox.SelectionMode = SelectionMode.None;
            }
            else
            {
                List<string> ls = new List<string>();
                foreach (Claim c in quoteHandler.Drivers[driverIndex].Claims)
                {
                    ls.Add(c.Date.ToShortDateString());
                }

                claimsListBox.DataSource = ls;
                claimsListBox.SelectionMode = SelectionMode.One;
            }
        }

        /// <summary>
        /// Updates the contents of the drivers list box.
        /// </summary>
        private void UpdateDriverList()
        {
            var index = driversListBox.SelectedIndex;
            var last = quoteHandler.Drivers[index];
            BindingList<string> d = (BindingList<string>)driversListBox.DataSource;
            d[index] = last.Name + " - " + last.Occupation + " - " + last.DateOfBirth.ToShortDateString();
            driversListBox.DataSource = d;
            Console.Write(last.Name);
        }

        /// <summary>
        /// Populates the drivers list box with the drivers that have been added.
        /// </summary>
        private void PopulateDriverList()
        {
            if (quoteHandler.Drivers.Count == 0)
            {
                BindingList<string> emptylist = new BindingList<string>();
                emptylist.Add("No drivers");
                driversListBox.DataSource = emptylist;
                driversListBox.SelectionMode = SelectionMode.None;
            }
            else
            {
                BindingList<string> ls = new BindingList<string>();
                foreach (Driver d in quoteHandler.Drivers)
                {
                    ls.Add(d.Name + " - " + d.Occupation + " - " + d.DateOfBirth.ToShortDateString());
                }

                driversListBox.DataSource = ls;
                driversListBox.SelectionMode = SelectionMode.One;
            }
        }

        /// <summary>
        /// Invoked when the Get Quote button is clicked.
        /// Updates the quote value label with the result of the premium calculation
        /// based on the current user input.
        /// </summary>
        /// <param name="sender">The source of the click event</param>
        /// <param name="e">The event arguments</param>
        private void GetQuoteButton_Click(object sender, EventArgs e)
        {
            var premiumString = quoteHandler.GetPremiumString();
            premiumValueLabel.Text = premiumString;
            if (premiumString.Contains("£"))
            {
                premiumLabel.Text = "Their Premium";
            }
            else
            {
                premiumLabel.Text = "Declined:";
            }
        }

        /// <summary>
        /// Invoked when the Clear button is clicked.
        /// Asks the user to confirm the operation with a message box.
        /// If the user clicks yes, clears all input and resets the form to a blank state.
        /// </summary>
        /// <param name="sender">The source of the click event</param>
        /// <param name="e">The event arguments</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to clear the form?", "Clear", MessageBoxButtons.YesNo);
            if (d.Equals(DialogResult.Yes))
            {
                startDatePicker.Value = DateTime.Today;
                driverNameField.Text = string.Empty;
                occupationComboBox.Text = string.Empty;
                dateOfBirthPicker.Value = DateTime.Today;
                newClaimDatePicker.Value = DateTime.Today;
                quoteHandler = new QuoteHandler();
                quoteHandler.StartDate = startDatePicker.Value;
                driversListBox.ClearSelected();
                driverDetailsGroupBox.Enabled = false;
                PopulateClaimList();
                PopulateDriverList();
                premiumLabel.Text = "Their premium";
                premiumValueLabel.Text = "£--.--";
            }
        }

        /// <summary>
        /// Invoked when the Remove Driver button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event arguments</param>
        private void DeleteDriverButton_Click(object sender, EventArgs e)
        {
            if (!quoteHandler.removeDriver(driversListBox.SelectedIndex))
            {
                MessageBox.Show("Could not remove driver", "Error");
            }
            PopulateDriverList();
            PopulateClaimList();
            PopulateDriverDetails();
        }

        /// <summary>
        /// Update the driver details fields 
        /// </summary>
        private void PopulateDriverDetails()
        {
            if(quoteHandler.Drivers.Count == 0)
            {
                driverNameField.Text = string.Empty;
                occupationComboBox.Text = string.Empty;
                dateOfBirthPicker.Value = DateTime.Today;
                driverDetailsGroupBox.Enabled = false;
                return;
            }

            driverNameField.Text = quoteHandler.Drivers[driversListBox.SelectedIndex].Name;
            occupationComboBox.Text = quoteHandler.Drivers[driversListBox.SelectedIndex].Occupation;
            dateOfBirthPicker.Value = quoteHandler.Drivers[driversListBox.SelectedIndex].DateOfBirth;

        }

        /// <summary>
        /// Autogenerated method
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event arguments</param>
        private void Main_Load(object sender, EventArgs e)
        {
            //nothing
        }

        /// <summary>
        /// Invoked when the policy start date picker is updated
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event arguments</param>
        private void StartDate_Updated(object sender, EventArgs e)
        {
            quoteHandler.StartDate = startDatePicker.Value;
        }

        private void DeleteClaimButton_Click(object sender, EventArgs e)
        {
            if (!quoteHandler.Drivers[driversListBox.SelectedIndex].RemoveClaim(claimsListBox.SelectedIndex))
            {
                MessageBox.Show("Could not remove claim", "Error");
                return;
            }
            PopulateClaimList();
        }
    }
}
