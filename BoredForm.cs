using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeniorProject_CSC490_
{
    public partial class BoredForm : Form
    {
        public BoredForm()
        {
            InitializeComponent();
        }

        private void ActivityButton_Click(object sender, EventArgs e)
        {
            //make a request to the API
            var client = new RestClient("https://www.boredapi.com/api/activity");
            var request = new RestRequest("https://www.boredapi.com/api/activity");
            var response = client.Execute(request);

            //Gets the content of the request
            string rawResponse = response.Content;
            
            //Creates a dynamic object and stores the data from the json file as a serilized array which then can be accessed using the key of the array 
            var boredObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(rawResponse);
          
            //Displays activity info in text field 
            BoredTextBox.Text = "Activity: " + boredObj.activity.ToString() + "." + "\r\n" + "Type: " + boredObj.type.ToString() + "\r\n" + "Number of people: " + boredObj.participants.ToString();
            SaveOver.Enabled = true;
            SaveAppend.Enabled = true;
        }

        //quit button
        private void QuitButtonBored_Click(object sender, EventArgs e)
        {
            //closes Boredom app
            this.Close();
        }

      
        //save button
        private void SaveAppend_Click(object sender, EventArgs e)
        {
            SaveForm Save = new SaveForm(this);
            Save.Show();
        }

        //save as button
        private void SaveOver_Click(object sender, EventArgs e)
        {
            //opens a windows save dialog box and allows the user to save where they want and name the file what they want 
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.FilterIndex = 1;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //writes text to file that they choose 
                    File.WriteAllText(sfd.FileName, BoredTextBox.Text);
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //pop up message box that displays a help message for user
            MessageBox.Show("Press the button labeled 'Get Activity' to generate a random activity for you and/or others to do."+
                "\r\n" + "The activity, type of activity, and number of people will be displayed for you." + 
                "\r\n" + "You can save the activity in a location you choose with the 'Save As' menu function or you can append a file you already have in the 'MyDocuments' folder using the 'Save' menu fucntion." , "Help Menu");
        }
    }
}
