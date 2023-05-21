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
    public partial class SaveCat : Form
    {
       
        
        
        readonly Form _Frm;
        public SaveCat(Form SaveCat)
        {
            InitializeComponent();
            _Frm = SaveCat;
        }

        private void SaveButtonCat_Click(object sender, EventArgs e)
        {
           CatForm objmain = (CatForm)_Frm;

            //code to get document path
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //get text from fileName Field 
            string fileName = FileNameCat.Text;


            //logic to block these symbols for the file name as windows does not allow file names with thess symbols in it  \ / : * ? " < > |
            try
            {
                if (fileName.Contains("\\") || fileName.Contains("?") || fileName.Contains("/") || fileName.Contains(">") || fileName.Contains(":") || fileName.Contains("|") || fileName.Contains("<") || fileName.Contains('"'))
                {
                    ErrorFieldSaveCat.Text = "Please input a valid file name.";
                }
                else
                {
                    //set error field to nothing on valid input
                    ErrorFieldSaveCat.Text = "";


                    var saveText = objmain.CatTextBox.Text + "\r\n";

                    // using streamwriter object to write to a file called shimlabel
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, fileName + ".txt"), append: true))
                    {
                        //write to the shimlabel file
                        outputFile.WriteLine(saveText);

                        //close the streamwriter object
                        outputFile.Close();

                        MessageBox.Show("Your data has been successfully stored.");
                    }
                }
            }
            catch (IOException)
            {
                //error message for file names that are too long
                ErrorFieldSaveCat.Text = "Please input a file name under 256 characters.";

            }
           
        }

        private void SaveQuitButtonCat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
