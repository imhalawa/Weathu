using System;
using System.Windows.Input;

namespace WeatherApp.ViewModels.Commands
{
    class FetchWeatherCommand : ICommand
    {

        private WeatherVM _weatherVM;
        public FetchWeatherCommand(WeatherVM weatherVM)
        {
            _weatherVM = weatherVM;
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            // The Parameter can be anything, it's the ICommand way to validate whether to click or not
            // In this case it will be bound to the TextBox Content via the CommandParameters Property
            var query = parameter as string;
            return !string.IsNullOrWhiteSpace(query);
        }

        public async void Execute(object parameter)
        {

            await _weatherVM.FetchWeather();
        }
    }
}
