/**
 * 
 * Elliott Holliday
 * 5/11/2023
 * Main Menu Program that allowes user to use 5 different apps that each do something different 
 * Shim Adder app, Cat fact app, Bored app, Weather app, and Timer app are included in this program
 *
 */




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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        //initalizes the date and time based on the system time on form load and starts the timer to keep current time
        private void MainMenu_Load(object sender, EventArgs e)
        {
            MainMenuDate.Text = DateTime.Now.ToString("dddd, MMM dd, yyyy");
            MainMenuTime.Text = DateTime.Now.ToLongTimeString();
            MainMenuTimer.Start();

        }
        //Timmer tick that updates time in the MainMenuTime label
        private void MainMenuTimer_Tick(object sender, EventArgs e)
        {
            MainMenuTime.Text = DateTime.Now.ToLongTimeString();
        }


        //Quit button 
        private void QuitMainMenuButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //opens up boredom app
        private void BoredMenuButton_Click(object sender, EventArgs e)
        {
            BoredForm BoredForm = new BoredForm();
            BoredForm.Show();

        }

        //opens cat app
        private void CatMenuButton_Click(object sender, EventArgs e)
        {
            CatForm CatForm = new CatForm();
            CatForm.Show();
        }

        //opens weather app
        private void WeatherMenuButton_Click(object sender, EventArgs e)
        {
            WeatherForm weatherForm = new WeatherForm();
            weatherForm.Show();
        }
        
        //opens timers app
        private void TimerMenuButton_Click(object sender, EventArgs e)
        {
            TimerForm timerForm = new TimerForm();
            timerForm.Show();
        }

        //opens shim app
        private void ShimMenuButton_Click(object sender, EventArgs e)
        {
            ShimForm shimForm = new ShimForm();
            shimForm.Show();
        }
    }
}
