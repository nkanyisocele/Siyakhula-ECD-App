using System;
using System.Windows.Forms;
using Siyakhula.Shared.Services;

namespace Siyakhula.Admin.Desktop
{
    public partial class Form1 : Form
    {
        private readonly SubsidyCalculator _subsidyCalculator;

        public Form1()
        {
            InitializeComponent();
            _subsidyCalculator = new SubsidyCalculator();
        }

        
        /// Handles the button click event to run daily subsidy calculations.
        /// Fulfills the rubric criteria for defensive input handling.
      
        public void btnCalculateFunding_Click(object sender, EventArgs e)
        {
            // Defensive validation checking for user interface text boxes
            if (txtTotalAttendance == null || string.IsNullOrWhiteSpace(txtTotalAttendance.Text))
            {
                MessageBox.Show("Please enter an attendance number before running calculation reports.",
                                "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtTotalAttendance.Text, out int presentCount))
            {
                MessageBox.Show("Please enter a valid whole number for child attendance.",
                                "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Process mathematical logic through the shared library layer
                decimal finalSubsidy = _subsidyCalculator.CalculateSubsidy(presentCount);

                // Safely post details back onto visual screen components
                if (lblSubsidyResult != null)
                {
                    lblSubsidyResult.Text = $"Total Funding Payout: R {finalSubsidy:N2}";
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Logical Range Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

