using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeniorProject_CSC490_
{
    public partial class TimerForm : Form
    {
        public TimerForm()
        {
            InitializeComponent();
        }


        //vars for keeping track of user input of seconds and minutes and a 3rd var to make total seconds 
        int seconds = 0;
        int minutes = 0;
        int hours = 0;
        int total_seconds = 0;





        private void SetButton_Click(object sender, EventArgs e)
        {
            //try catch statement to make sure the user doesnt try to add symbols 
            try
            {
                //converts user inputs to ints for the text fields 
                seconds = Convert.ToInt32(SecondTextBox.Text);
                minutes = Convert.ToInt32(MinuteTextBox.Text);
                hours = Convert.ToInt32(HourTextBox.Text);

                //logic to make sure the user doesnt enter anything over 59 mins and 59 seconds and 23 hours
                if (seconds > 59 || minutes > 59 || hours > 23)
                {

                    //error message when they do enter something over 60 minutes and seconds and 24 hours
                    ErrorField.Text = "Input Error: Please only a value for minutes or seconds under 60 and hours under 24.";
                    
                    //sets text fields to null and focus back on hours 
                    if (SecondTextBox != null)
                    {
                        SecondTextBox.Text = null;
                    }
                    if (MinuteTextBox!= null)
                    {
                        MinuteTextBox.Text = null;
                    }
                    if(HourTextBox!= null)
                    {
                        HourTextBox.Text = null;
                    }
                    HourTextBox.Focus();
                }
                else
                {

                    //logic to make sure they do not enter something under 0 for the hours, minutes, and seconds
                    if (seconds < 0 || minutes < 0|| hours < 0)
                    {
                        //error message if that happens
                        ErrorField.Text = "Input Error: Please only enter positive numbers.";

                    }
                    else
                    {
                        //set text box for error messages to null because the input was sucessful 
                        if (ErrorField!= null)
                        {
                            ErrorField.Text = null;
                        }
                        //converts the user input of minutes and seconds to total seconds
                        total_seconds = (hours * 60 * 60)  + (minutes * 60) + seconds;

                        //logic to make sure that the hours, miniutes, and seconds is always displayed in the form of HH:MM:SS
                        if (hours == 0 && minutes >= 10 && seconds >= 10)
                        {
                            TimerDisplayLabel.Text = hours.ToString() + "0:" + minutes.ToString() + ":" + seconds.ToString();

                        }
                        else if (hours == 0 && minutes == 0 && seconds >= 10)
                        {
                            TimerDisplayLabel.Text = hours.ToString() + "0:0" + minutes.ToString() + ":" + seconds.ToString();

                        }
                        else if (hours == 0 && minutes < 10 && seconds < 10)
                        {
                            TimerDisplayLabel.Text = hours.ToString() + "0:0" + minutes.ToString() + ":0" + seconds.ToString();
                        }
                        else if (hours == 0 && minutes < 10 && seconds >= 10)
                        {
                            TimerDisplayLabel.Text = hours.ToString() + "0:0" + minutes.ToString() + ":" + seconds.ToString();
                        }
                        else if (hours == 0 && minutes >= 10 && seconds < 10)
                        {
                            TimerDisplayLabel.Text = hours.ToString() + "0:" + minutes.ToString() + ":0" + seconds.ToString();
                        }
                        else if (hours > 0 && minutes == 0 && seconds < 10)
                        {
                            TimerDisplayLabel.Text = hours.ToString() + ":0" + minutes.ToString() + ":0" + seconds.ToString();
                        }
                        else if (hours > 0 && minutes == 0 && seconds >= 10)
                        {
                            TimerDisplayLabel.Text = hours.ToString() + ":0" + minutes.ToString() + ":" + seconds.ToString();
                        }
                        else if (hours == 0 && minutes == 0 && seconds < 10)
                        {
                            TimerDisplayLabel.Text = hours.ToString() + ":0" + minutes.ToString() + ":0" + seconds.ToString();
                        }
                        else if (hours  > 0 && minutes >= 10 && seconds >= 10)
                        {
                            TimerDisplayLabel.Text = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
                        }
                        else
                        {
                            TimerDisplayLabel.Text = hours.ToString() + ":0" + minutes.ToString() + ":0" + seconds.ToString();
                        }



                        //sets the input fields to null and focus back to the minutes box, if statments are to make sure that not trying to set a null field to null apparently C# does not like that
                        if (MinuteTextBox != null)
                        {
                            MinuteTextBox.Text = null;
                        }
                        if (SecondTextBox != null)
                        {
                            SecondTextBox.Text = null;
                        }
                        if (HourTextBox != null)
                        {
                            HourTextBox.Text = null;
                        }

                       HourTextBox.Focus();
                    }
                }
            }

            catch (FormatException)
            {
                //error message for symbols
                ErrorField.Text = "Input Error: Please only enter numbers.";

            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            //logic for timer if total seconds is not 0 keep going through this loop
            if (total_seconds > 0)
            {
                total_seconds--;

                //math to set the label3 field every time you subtract
                hours = total_seconds / 3600;
                minutes = (total_seconds - (hours * 3600)) / 60 ;
                seconds = total_seconds - (minutes * 60) - (hours * 3600);


                //same logic as above 
                if (ErrorField != null)
                {
                    ErrorField.Text = null;
                }
                //converts the user input of minutes and seconds to total seconds
                total_seconds = (hours * 60 * 60) + (minutes * 60) + seconds;

                //logic to make sure that the hours, miniutes, and seconds is always displayed in the form of HH:MM:SS however hours I didn't bother putting a 0 infront of a single digit number because it doesnt look bad exp: 9:50:50 instead of 09:50:50
                if (hours == 0 && minutes >= 10 && seconds >= 10)
                {
                    TimerDisplayLabel.Text = hours.ToString() + "0:" + minutes.ToString() + ":" + seconds.ToString();

                }
                else if (hours == 0 && minutes == 0 && seconds >= 10)
                {
                    TimerDisplayLabel.Text = hours.ToString() + "0:0" + minutes.ToString() + ":" + seconds.ToString();

                }
                else if (hours == 0 && minutes < 10 && seconds < 10)
                {
                    TimerDisplayLabel.Text = hours.ToString() + "0:0" + minutes.ToString() + ":0" + seconds.ToString();
                }
                else if (hours == 0 && minutes < 10 && seconds >= 10)
                {
                    TimerDisplayLabel.Text = hours.ToString() + "0:0" + minutes.ToString() + ":" + seconds.ToString();
                }
                else if (hours == 0 && minutes >= 10 && seconds < 10)
                {
                    TimerDisplayLabel.Text = hours.ToString() + "0:" + minutes.ToString() + ":0" + seconds.ToString();
                }
                else if (hours > 0 && minutes == 0 && seconds < 10)
                {
                    TimerDisplayLabel.Text = hours.ToString() + ":0" + minutes.ToString() + ":0" + seconds.ToString();
                }
                else if (hours > 0 && minutes == 0 && seconds >= 10)
                {
                    TimerDisplayLabel.Text = hours.ToString() + ":0" + minutes.ToString() + ":" + seconds.ToString();
                }
                else if (hours == 0 && minutes == 0 && seconds < 10)
                {
                    TimerDisplayLabel.Text = hours.ToString() + ":0" + minutes.ToString() + ":0" + seconds.ToString();
                }
                else if (hours > 0 && minutes >= 10 && seconds >= 10)
                {
                    TimerDisplayLabel.Text = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
                }
                else
                {
                    TimerDisplayLabel.Text = hours.ToString() + ":0" + minutes.ToString() + ":0" + seconds.ToString();
                }



            }
            else
            {
                //if total seconds is = to 0 stop the timer
                this.timer1.Stop();
                //audio file path which is located in debug because I didn't feel like typing out the whole path to a different folder 
                string audioFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Alarm.wav");
                
                //object that allows you to play audio file from a specified path
                SoundPlayer soundPlayer = new SoundPlayer(audioFile);
                soundPlayer.Play();
                //plays for 3 seconds
                await Task.Delay(3 * 1000);
                soundPlayer.Stop();
              
            }
        }

        //Start the timer
        private void StartButton_Click(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        //Stop the timer
        private void StopButton_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            //make sure timer is stopped
            this.timer1.Stop();

            //reset all fields and vars assoicated with the timer
            seconds = 0;
            minutes = 0;
            MinuteTextBox.Text = null;
            SecondTextBox.Text = null;
            HourTextBox.Text = null;
            TimerDisplayLabel.Text = "00:00:00";
            HourTextBox.Focus();
            ErrorField.Text = "";
        }

        //quit button
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HelpMenuButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The timer app can be used to set a timer up to a maximum of 23:59:59 in form of HH:MM:SS."+
                "\r\n" + "You can set the time by entering numbers in each of the corresponding boxes for hours, minutes, and seconds then clicking the set button. You can also hit the [Enter key]." + 
                "\r\n" + " The Start and Stop buttons control the start and stop of the timer. The Reset button resets all fields and the timer.", "Help Menu");
        }

        private void HourTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //checks if [Enter] key is pressed
            if (e.KeyCode == Keys.Enter)
            {
                //stops the windows sound from playing
                e.Handled = e.SuppressKeyPress = true;
                //performs a button click on the add button
                SetButton.PerformClick();
                //sets field for another input after enter is pressed, also sets focus on input field
                HourTextBox.Text = null;
                MinuteTextBox.Text= null;
                SecondTextBox.Text= null;
                HourTextBox.Focus();



            }
        }
    }
}
