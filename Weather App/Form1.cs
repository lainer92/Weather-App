using Newtonsoft.Json;
using System;
using System.Net;
using System.Windows.Forms;

namespace Weather_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //APIKey содержит ключ для доступа к API сервису погоды OpenWeatherMap
        string APIKey = "de0bb88417b1c1eb6975a2b698772976";

        //Метод buttonSearch_Click срабатывает при нажатии кнопки 'поиск'
        //и вызывает метод getWeather для получения данных о погоде.
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            //В случае возникновения ошибок, обрабатываются исключения
            //и выводится соответствующее окно с сообщением об ошибке
            try
            {
                getWeather();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Метод getWeather отправляет HTTP запрос к API, получает JSON ответ,
        //парсит его с помощью библиотеки JSON.NET и обновляет интерфейс с помощью полученных данных
        void getWeather()
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}", textBoxCity.Text, APIKey);
                    var json = webClient.DownloadString(url);
                    WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                    pictureBoxIcon.ImageLocation = "http://api.openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                    labelMain.Text = Info.weather[0].main;
                    labelDescription.Text = Info.weather[0].description;

                    labelWindSpeed.Text = Info.wind.speed.ToString("0.#");

                    double celcTemp = Info.main.temp - 272.1;
                    labelTemp.Text = celcTemp.ToString("0.#");
                }
                catch (WebException webEx)
                {
                    MessageBox.Show("WebException: " + webEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (JsonException jsonEx)
                {
                    MessageBox.Show("JsonException: " + jsonEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
