using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeniorProject_CSC490_
{
    public partial class BundleLimitForm : Form
    {
        ShimForm _frm;
        public BundleLimitForm(ShimForm frm)
        {
            InitializeComponent();
            _frm = frm;
        }
        
        //var for input from textbox
        double input;

        //okay button sets the bundle limit on the shim form
        private void OkayButton_Click(object sender, EventArgs e)
        {
            //create an object with form1 inorder to access its vars and text fields 
            ShimForm objMain = (ShimForm)_frm;


            //error handling
            try
            {
                //convert input to a double
                input = Convert.ToDouble(BundleLimitSet.Text);

                //if statments that looks for inputs less than 0 and displays error message if one is found
                if (input <= 0)
                {
                    //error to make sure input is over 0
                    ErrorFieldBundleLimit.Text = "Only input numbers greater than 0";
                }
                else
                {
                    //uses some objects to acces other fields and variables in other fields
                    objMain.BundleLimitCount.Text = input.ToString();
                    objMain.BundleLimit.Text = input.ToString();
                    objMain.limitTillFull = input;

                    //resets all the fields incase the user has added a number before they have changed the bundle limit
                    objMain.ResetButton.PerformClick();
                    this.Hide();
                }
            }
            catch (FormatException)
            {
                //error message for symbols
                ErrorFieldBundleLimit.Text = "Input Error: Input only numbers";

            }
        }


        //cancel button closes the form
        private void CancelButtonBundleLimit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BundleLimitSet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //stops the windows sound from playing
                e.Handled = e.SuppressKeyPress = true;
                //performs a button click on the add button
                OkayButton.PerformClick();
                //sets field for another input after enter is pressed, also sets focus on input field
                BundleLimitSet.Text = null;
                BundleLimitSet.Focus();

                

            }
        }

        //sets focus on input when form is opened
        private void BundleLimitForm_Load(object sender, EventArgs e)
        {
            BundleLimitSet.Focus();
        }
    }
}
