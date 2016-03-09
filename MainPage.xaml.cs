using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UWP_WebService.WeatherService;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_WebService
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            zipCode.Text = "28523";
        }

        private async void OnRefresh(object aSender, RoutedEventArgs aE)
        {
            var proxy = new WeatherService.WeatherSoapClient();

            var result = await proxy.GetCityWeatherByZIPAsync(zipCode.Text);
            var fr = await proxy.GetCityForecastByZIPAsync(zipCode.Text);
            var f = fr.ForecastResult;
            if (!result.Success) return;

            zipResult.Text = string.Format("{0}, {1}", result.City, result.State);
            var message = $"\n\nConditions - {result.Description} \n\nTemperature - {result.Temperature} \n\nRelative Humidity - {result.RelativeHumidity} \n\nWind - {result.Wind} \n\nPressure - {result.Pressure} - \n\nPressure - {fr.WeatherStationCity}- \n\nWind Chill - {result.WindChill}- \n\nVisibility - {result.Visibility}";
            var messageDialog = new MessageDialog(message, "Weather");
            await messageDialog.ShowAsync();
        }
    }
}
