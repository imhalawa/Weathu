using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using WeatherApp.Models;
using WeatherApp.ViewModels.Commands;
using WeatherApp.ViewModels.Helpers;

namespace WeatherApp.ViewModels
{
    class WeatherVM : INotifyPropertyChanged
    {

        public WeatherVM()
        {

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {

                CityName = "Cairo";
                Result = new OpenWeatherResponse()
                {

                    wind = new Wind() { speed = 4 },
                    main = new Main() { temp = 45, temp_max = 46, temp_min = 44, humidity = 60 },
                    sys = new Metadata { country = "EG" },
                    name = "Cairo",
                    clouds = new Clouds { all = 10 },
                };
            }


            // Constructing the Fetch Command
            FetchWeatherCommand = new FetchWeatherCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Properities
        private string _cityName;
        public string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;
                OnPropertyChanged();

            }
        }


        private OpenWeatherResponse _result;
        public OpenWeatherResponse Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged();

            }
        }


        // Functions
        public async Task FetchWeather()
        {
            try
            {

                var result = await OpenWeatherHelper.GetCurrentWeather(CityName);
                result.weather[0].icon = OpenWeatherHelper.GetImageUrl(result.weather[0].icon);
                Result = result;
            }
            catch (JsonException ex)
            {
                MessageBox.Show("City not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }


        // Commands
        public FetchWeatherCommand FetchWeatherCommand { get; set; }
    }
}
