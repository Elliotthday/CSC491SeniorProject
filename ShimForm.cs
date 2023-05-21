using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeniorProject_CSC490_
{
    public partial class ShimForm : Form
    {
        public ShimForm()
        {
            InitializeComponent();
        }

        private void ShimForm_Load(object sender, EventArgs e)
        {
            UndoButton.Enabled = false;
        }


        //def and inital of sum as a global variable for use in both buttons
        double sum = 0;
        
        //var to keep track of number of shims added
        double count_Shims = 0;
        
        //var to take the input from the input field
        double input;
        
        //var for keeping track of max
        double max = 0;
        
        //var for keeping track of min
        double min = 0;
        
        //var for keeping track of average
        double average = 0;
        
        //List to hold inputs from user 
        List<Double> standardDev = new List<double>();
        
        //var for standard deviation
        double Standardsum = 0;
        
        //var to keep track of bundle limit
        public double limitTillFull = 5;






        //Reset Button
        private void ResetButton_Click(object sender, EventArgs e)
        {
            //clears the List for new inputs
            standardDev.Clear();

            //sets sum to zero so the user can start over
            sum = 0;
            count_Shims = 0;
            average = 0;
            max = 0;
            min = 0;

            //sets limit var to the bundle full field
            limitTillFull = Convert.ToDouble(BundleLimit.Text);
            
            //sets bundle full field to the correct number
            BundleLimitCount.Text = BundleLimit.Text;


            //sets other information fields back to 0 for the user
            RunningTotal.Text = sum.ToString();
            ShimCount.Text = count_Shims.ToString();
            MinWeight.Text = "0";
            MaxWeight.Text = "0";
            AverageWeight.Text = "0";




            //sets focus back on the input field for the user
            InputField.Text = null;
            InputField.Focus();
            
            //set error field to null
            ErrorField.Text = null;
            
            //set standard dev field to null
            StandardDev.Text = "0";
            
            //enables undo button after reset is pressed
            UndoButton.Enabled = false;
        }










        //Undo Button
        private void UndoButton_Click(object sender, EventArgs e)
        {
            //subtracts the last input and and displays it in the output field
            sum -= input;
            RunningTotal.Text = sum.ToString();

            //function to calculate standard Deviation
            try
            {

                //removes the last element added to the list 
                standardDev.RemoveAt(standardDev.Count - 1);

                double average = standardDev.Average();
                
                //gets the sum of the difference squares of each element in the list
                double sum = standardDev.Sum(d => Math.Pow(d - average, 2));
                
                //takes the square root of sum divided by the number of elements in the list minus 1
                Standardsum = Math.Sqrt((sum) / (standardDev.Count - 1));
            }
            catch (InvalidOperationException)
            {

                ErrorField.Text = "Please use reset button and not the undo when only one input has been entered";

            }


            //sets max and min back to what it was 
            max = standardDev.Max();
            min = standardDev.Min();

            //reverts changes to the till full field and var
            limitTillFull += input;
            BundleLimit.Text = limitTillFull.ToString();


            //displays max and min weight
            MaxWeight.Text = max.ToString();
            MinWeight.Text = min.ToString();


            //display standard dev in its field 
            StandardDev.Text = Math.Round(Standardsum,5).ToString();

            //subtracks 1 from the total shims and displays it in the output field
            count_Shims = --count_Shims;
            ShimCount.Text = count_Shims.ToString();
            average = sum / count_Shims;


            //displays average weight
            AverageWeight.Text = average.ToString();
            
            //sets the focus to the input field
            InputField.Focus();
            
            //sets error field to null
            ErrorField.Text = null;
            InputField.Text = null;
            
            //disables undo button so user cannot hit it multiple times
            UndoButton.Enabled = false;
        }







        //add button
        private void AddButton_Click_1(object sender, EventArgs e)
        {
            //try block to check for input errors
            try
            {

                //takes the number to add and converts it from text to double, then assigns it to input
                input = Convert.ToDouble(InputField.Text);


                //allows for the first input to be min because var is set to 0;
                if (min <= 0)
                {
                    min = input;

                }

                //checks if user is going over bundle limit will display error message if they have gone over the limit
                if ((limitTillFull - input) < 0)
                {
                    ErrorField.Text = "Input exceeds bundle limit";

                    if (count_Shims >= 2)
                    {
                        //enables undo button after an input is made
                        UndoButton.Enabled = true;

                    }
                    //sets the input field to null for another data entry and sets the focus on the input field
                    InputField.Text = null;
                    InputField.Focus();
                }
                else
                {
                    //if statments that looks for inputs less than 0 and displays error message if one is found
                    if (input <= 0)
                    {
                        ErrorField.Text = "Only input numbers greater than 0";
                    }
                    else
                    {
                        //check for max number in input
                        if (input > max)
                        {
                            max = input;
                        }
                        //check for min number in input
                        if (input < min)
                        {
                            min = input;
                        }

                        //subtracts input from the limit on bundle
                        limitTillFull -= input;

                        //takes the input var and adds it to the sum then set sum equal to that number
                        sum += input;
                        count_Shims++;

                        //set error field to null
                        ErrorField.Text = null;

                        //calculate average
                        average = sum / count_Shims;

                        //adds input to list
                        standardDev.Add(input);

                        //fucntion to calculate Standard deviation
                        if (standardDev.Any())
                        {
                            //gets the average of the values in the list
                            double average = standardDev.Average();

                            //gets the sum of the difference squares of each element in the list
                            double sum = standardDev.Sum(d => Math.Pow(d - average, 2));

                            //takes the square root of sum divided by the number of elements in the list minus 1
                            Standardsum = Math.Sqrt((sum) / (standardDev.Count - 1));
                        }
                    }

                    if (count_Shims >= 2)
                    {
                        //enables undo button after an input is made
                        UndoButton.Enabled = true;

                    }

                    //displays standard deviation
                    StandardDev.Text = Math.Round(Standardsum,5).ToString();

                    //displays ammount left till full
                    BundleLimitCount.Text = limitTillFull.ToString();

                    //displays max weight
                    MaxWeight.Text = max.ToString();

                    //displays min weight
                    MinWeight.Text = min.ToString();

                    //displays average weight
                    AverageWeight.Text = average.ToString();

                    //displays the sum var and total shims in the correspoding output fields
                    RunningTotal.Text = sum.ToString();
                    ShimCount.Text = count_Shims.ToString();

                    //sets the input field to null for another data entry and sets the focus on the input field
                    InputField.Text = null;
                    InputField.Focus();
                }
            }

            //catch block to show a message box that tells user about an invald input format
            catch (FormatException)
            {
                ErrorField.Text = "Input Error: Input only numbers";
            }
        }



        //bundle limit menu button
        private void BundleLimitButton_Click(object sender, EventArgs e)
        {
            //creates new form passing Shim from to the constructor for bundlelimitform to access shim form data
            BundleLimitForm bundleLimitForm = new BundleLimitForm(this);
            bundleLimitForm.Show();

        }

        //quit button
        private void QuitButtonShim_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //allows user to press enter to input numbers
        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            //checks if [Enter] key is pressed
            if (e.KeyCode == Keys.Enter)
            {
                //stops the windows sound from playing
                e.Handled = e.SuppressKeyPress = true;
                //performs a button click on the add button
                AddButton.PerformClick();
                //sets field for another input after enter is pressed, also sets focus on input field
                InputField.Text = null;
                InputField.Focus();

                

            }
        }



        //help menu button
        private void HelpButtonShim_Click(object sender, EventArgs e)
        {
            //pop up message box that displays a help message for user
            MessageBox.Show("Input the weight of shims in the 'Weight of Shim To Add' input field. \r\nPress [Enter] or the Add button to add the weight to the running total. This number will be displayed" +
                " in the 'Running Total Weight' field.\r\nA shim count will also be kept track of each time you add a shim to the running total. This will be displayed in the 'Count of Shims' field." +
                "\r\nTo undo the last weight added press the Undo button. In order to do another undo you need to make another valid input." +
                "\r\nTo reset the running total and the count press the Reset button." + 
                "\r\n" + "The bundle limit will allow you to change the bundle weight limit, the defult is 5 lbs."+
                "\r\n" + "Save will allow you to save under a specific file name as a txt file. Print will print out a label with a time stamp and all the data from the data fields.", "Help Menu");
        }



        //save ass button
        private void SaveAsButton_Click(object sender, EventArgs e)
        {


            //gets all the data from the fields and sets them to strings
            string shim_CountTotal = count_Shims.ToString();
            string shim_weightTotal = sum.ToString();
            string max_Weight = max.ToString();
            string min_Weight = min.ToString();
            string standardDevWeight = Standardsum.ToString();
            string avgWeight = average.ToString();
            
            //gets local time and date from machine 
            DateTime timeStamp = DateTime.Now;

            //lable to be saved to file including total shim weight, count, and date and time
            string saveText = "Total Weight of Shims (lbs): " + shim_weightTotal + "\r\n\r\n" + "Number of Shims: " + shim_CountTotal + "\r\n\r\n" + "Minimum Weight (lbs): " + min_Weight + "\r\n\r\n" + "Maximum Weight (lbs): " + max_Weight + "\r\n\r\n" + "Average Weight (lbs): "
                + avgWeight + "\r\n\r\n" + "Standard Deviation: " + standardDevWeight + "\r\n\r\n" + timeStamp.ToString();


            //save dialog to allow user to save a label 
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            //gives text file options to save
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.FilterIndex = 1;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //writes text to file that they choose 
                    File.WriteAllText(sfd.FileName, saveText);
                }
            }
        }



        //print button
        private void PrintButton_Click(object sender, EventArgs e)
        {
            //setting for print dialog 
            printDialog1.AllowSomePages = true;
            printDialog1.ShowHelp = true;

            //name for document to print
            printDocument1.DocumentName = "My Document";
          
            //set the print dialog document to a print document to be handled the print page event
            printDialog1.Document = printDocument1;

            //shows the print dialog 
            DialogResult result = printDialog1.ShowDialog();

            // If the result is OK then print the document.
            if (result == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }


        //print page even handler 
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //gets all the data from the fields and sets them to strings
            string shim_CountTotal = count_Shims.ToString();
            string shim_weightTotal = sum.ToString();
            string max_Weight = max.ToString();
            string min_Weight = min.ToString();
            string standardDevWeight = Standardsum.ToString();
            string avgWeight = average.ToString();

            //gets local time and date from machine 
            DateTime timeStamp = DateTime.Now;

            //label to be printed out includes every field of data 
            string saveText = "Total Weight of Shims (lbs): " + shim_weightTotal + "\r\n\r\n" + "Number of Shims: " + shim_CountTotal + "\r\n\r\n" + "Minimum Weight (lbs): " + min_Weight + "\r\n\r\n" + "Maximum Weight (lbs): " + max_Weight + "\r\n\r\n" + "Average Weight (lbs): "
                + avgWeight + "\r\n\r\n" + "Standard Deviation: " + standardDevWeight + "\r\n\r\n" + timeStamp.ToString();
                
            //determines the font size and other pring features 
            e.Graphics.DrawString(saveText,new Font ("Arial",15,FontStyle.Bold), Brushes.Black, 10, 10);
        }
    }
}
