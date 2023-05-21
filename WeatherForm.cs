using Microsoft.CSharp.RuntimeBinder;
using RestSharp;
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
    public partial class WeatherForm : Form
    {
        public WeatherForm()
        {
            InitializeComponent();
        }

        private void GetWeatherButton_Click(object sender, EventArgs e)
        {
            //gets city from user input
            string city = CityTextBox.Text;
            try
            {
                //cases of 1 and 2 being able to pull a sepcific city for some reason
                if (CityTextBox.Text == "")
                {
                    WeatherTextBox.Text = "Please Input a city.";
                }
                else if (city.Contains("2") || city.Contains("1"))
                {
                    WeatherTextBox.Text = "Please Input only letters.";
                }
                else
                {


                    //sends request and gets response from the api using the city inputed
                    var client = new RestClient("https://api.weatherbit.io/v2.0/current?city=" + city + "&units=I&key=8561810711634d9ba34da99f4404054e");
                    var request = new RestRequest("https://api.weatherbit.io/v2.0/current?city=" + city + "&units=I&key=8561810711634d9ba34da99f4404054e");
                    var response = client.Execute(request);

                    //stores the content in a string 
                    string rawResponse = response.Content;
                    //stores the content into a object as a json serialized array
                    var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(rawResponse);


                    //get data from Json object, data[0] is to access the the first array in data because it is an single array of objects, then use the key to access the data from the api
                    WeatherTextBox.Text = "City Name: " + obj.data[0].city_name.ToString() + "\r\n" + "Time zone: " + obj.data[0].timezone.ToString() + "\r\n" + "Wind speed: " + obj.data[0].wind_spd.ToString() + " mph" +
                    "\r\n" + "Wind gust: " + obj.data[0].gust.ToString() + " mph" + "\r\n" + "Releative humidity: " + obj.data[0].rh.ToString() + "%" + "\r\n" + "UV index: " + obj.data[0].uv.ToString() + "\r\n"
                    + "Air quality index: " + obj.data[0].aqi.ToString() + "\r\n" + "Precipitation: " + obj.data[0].precip.ToString() + " inch/hr" + "\r\n" + "Temperature: " + obj.data[0].temp.ToString() + "°F" + "\r\n"
                    + "Feels like: " + obj.data[0].app_temp.ToString() + "°F";

                }
            }
            catch (RuntimeBinderException)
            {
                WeatherTextBox.Text = "Please Input only letters.";
            }
           
        }

        //Quit button
        private void QuitWeatherApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HelpWeatherApp_Click(object sender, EventArgs e)
        {
            //pop up message box that displays a help message for user
            MessageBox.Show("Enter a major city in the correspoding field and click the button labled 'Get Weather'." + 
                "\r\n" + "The city name, time zone, wind speed, wind gust, releative humidity, UV index, air quality index, precipiotation, temperature, and apparent temperature will be displayed for you.", "Help Menu");
        }

        private void CityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //checks if [Enter] key is pressed
            if (e.KeyCode == Keys.Enter)
            {
                //stops the windows sound from playing
                e.Handled = e.SuppressKeyPress = true;
                //performs a button click on the add button
                GetWeatherButton.PerformClick();
                //sets city text box field to null and focus back on it
                CityTextBox.Text = null;
                CityTextBox.Focus();

            }
        }
    }
}
