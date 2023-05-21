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
    public partial class CatForm : Form
    {
        public CatForm()
        {
            InitializeComponent();
        }

        private void CatFactButton_Click(object sender, EventArgs e)
        {

            //makes request to api
            var client = new RestClient("https://catfact.ninja/fact");
            var request = new RestRequest("https://catfact.ninja/fact");
            var response = client.Execute(request);

            //Gets the content of the request
            string rawResponse = response.Content;
            //create an object to convert the json to an object we can access, yes I could of used a class and all that but for something so small and proof of concept
            //I just created a one use object for this purpose
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(rawResponse);
            //access the fact using the fact key
            var CatFact = obj.fact;
            //displays the fact in text box
            CatTextBox.Text = CatFact.ToString();
            
            //enable save buttons
            SaveAsCat.Enabled = true;
            SaveOverCat.Enabled = true;
            
            //get a random integer and assign it to an int
            Random randInt = new Random();
            int rand = randInt.Next(5);
            //change picture on cat app screen when button is clicked based off radom int
            CatPictureBox.Image = System.Drawing.Image.FromFile("C:/Users/james/source/repos/SeniorProject(CSC490)/SeniorProject(CSC490)/Properties/images/" + "cat" + rand +".png" );
        }

        private void SaveAsCat_Click(object sender, EventArgs e)
        {
            //opens a windows save dialog box and allows the user to save where they want and name the file what they want 
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
                    File.WriteAllText(sfd.FileName, CatTextBox.Text);
                }
            }
        }

        //opens save window for appending to a file in documents folder 
        private void SaveOverCat_Click(object sender, EventArgs e)
        {
            SaveCat Save = new SaveCat(this);
            Save.Show();
        }
        //quit button

        private void QuitCatApp_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press the button labeled 'Get Cat Fact' to generate a random cat fact for you." +
               "\r\n" + "The cat fact will be displayed for you." +
               "\r\n" + "You can save the cat fact in a location you choose with the 'Save As' menu function or you can append a file you already have in the 'MyDocuments' folder using the 'Save' menu fucntion.", "Help Menu");
        }

        private void CatTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //checks if [Enter] key is pressed
            if (e.KeyCode == Keys.Enter)
            {
                //stops the windows sound from playing
                e.Handled = e.SuppressKeyPress = true;
                //performs a button click on the add button
                CatFactButton.PerformClick();
              

            }
        }
    }
}
